using Core.Application.Communication.External.Messages;

namespace Core.Infrastructure.Communication.External.Messages;

internal sealed class MessageBus : IMessageBus
{
    private readonly IOutboxWriter _outboxWriter;

    public MessageBus(IOutboxWriter outboxWriter)
    {
        _outboxWriter = outboxWriter;
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage
    {
        var inserted = await _outboxWriter.InsertMessageAsync(message, cancellationToken);
        if (!inserted) throw new InvalidOperationException($"Failed to insert '{typeof(TMessage).FullName}' message into outbox");
    }
}