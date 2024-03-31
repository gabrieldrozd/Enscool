using MediatR;

namespace Core.Application.Communication.External.Messages;

public interface IMessageHandler<in TMessage> : INotificationHandler<TMessage>
    where TMessage : IMessage;