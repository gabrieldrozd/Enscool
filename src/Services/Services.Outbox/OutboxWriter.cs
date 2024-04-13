using Core.Application.Communication.External.Messages;
using Microsoft.Extensions.DependencyInjection;
using Services.Outbox.Database;
using Services.Outbox.OutboxMessages;

namespace Services.Outbox;

internal sealed class OutboxWriter : IOutboxWriter
{
    private readonly IServiceProvider _serviceProvider;

    public OutboxWriter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> InsertMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage
    {
        var outboxMessage = OutboxMessage.Create(message.MessageId, message.GetType(), message);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        await context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}