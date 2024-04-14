using Core.Application.Communication.External.Messages;
using Microsoft.Extensions.DependencyInjection;
using Services.Outbox.Database;
using Services.Outbox.OutboxMessages;

namespace Services.Outbox;

internal sealed class MessageBus : IMessageBus
{
    private readonly IServiceProvider _serviceProvider;

    public MessageBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage
    {
        var outboxMessage = OutboxMessage.Create(message.MessageId, message.GetType(), message);

        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OutboxDbContext>();
        await context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
        var result = await context.SaveChangesAsync(cancellationToken) > 0;
        if (!result)
        {
            throw new InvalidOperationException($"Failed to insert '{typeof(TMessage).FullName}' message into outbox");
        }
    }
}