using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Outbox.Database;
using Services.Outbox.OutboxMessages;

namespace Services.Outbox;

public class OutboxMessageProcessorJob : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public OutboxMessageProcessorJob(IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessMessagesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
        }
    }

    private async Task ProcessMessagesAsync(CancellationToken cancellationToken)
    {
        using var contextScope = _scopeFactory.CreateScope();
        var context = contextScope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        var outboxMessages = await context.OutboxMessages
            .Where(x => x.State == MessageState.Pending || x.State == MessageState.Failed)
            .OrderBy(x => x.CreatedOnUtc)
            .Take(20)
            .ToListAsync(cancellationToken);

        if (outboxMessages.Count == 0)
            return;

        foreach (var outboxMessage in outboxMessages)
        {
            try
            {
                using var publisherScope = _scopeFactory.CreateScope();
                var publisher = publisherScope.ServiceProvider.GetRequiredService<IPublisher>();
                await publisher.Publish(outboxMessage.Payload, cancellationToken);
            }
            catch (Exception ex)
            {
                outboxMessage.SetFailed(ex.Message);
                continue;
            }

            outboxMessage.SetProcessed();
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}