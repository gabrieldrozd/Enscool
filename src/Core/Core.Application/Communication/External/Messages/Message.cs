namespace Core.Application.Communication.External.Messages;

public abstract record Message(Guid MessageId) : IMessage;