using Core.Application.Communication.External.Messages;

namespace Core.Infrastructure.Communication.External.Messages;

internal sealed class MessageBus : IMessageBus
{
    private readonly InMemoryMessageQueue _queue;

    public MessageBus(InMemoryMessageQueue queue) => _queue = queue;

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default) where TMessage : IMessage
        => await _queue.Writer.WriteAsync(message, cancellationToken);
}