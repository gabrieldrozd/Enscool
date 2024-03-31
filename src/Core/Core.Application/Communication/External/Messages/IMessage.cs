using MediatR;

namespace Core.Application.Communication.External.Messages;

/// <summary>
/// Marker interface for messages.
/// </summary>
public interface IMessage : INotification
{
    public Guid MessageId { get; }
}