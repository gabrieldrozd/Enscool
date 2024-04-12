namespace Core.Application.Communication.External.Messages;

public interface IOutboxWriter
{
    Task<bool> InsertMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage;
}