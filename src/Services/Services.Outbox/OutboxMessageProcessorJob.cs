using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Outbox.Database;
using Services.Outbox.OutboxMessages;

namespace Services.Outbox;

public class OutboxMessageProcessorJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public OutboxMessageProcessorJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await ProcessMessagesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }

    private async Task ProcessMessagesAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();

        var outboxMessages = await context.OutboxMessages
            .Where(x => x.State == MessageState.Pending && x.ProcessedOnUtc == null)
            .OrderBy(x => x.CreatedOnUtc)
            .Take(20)
            .ToListAsync(cancellationToken);

        if (outboxMessages.Count == 0)
            return;

        var publisher = _serviceProvider.GetRequiredService<IPublisher>();
        foreach (var outboxMessage in outboxMessages)
        {
            try
            {
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