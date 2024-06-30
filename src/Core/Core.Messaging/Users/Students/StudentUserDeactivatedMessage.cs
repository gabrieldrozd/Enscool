using Core.Application.Communication.External.Messages;

namespace Core.Messaging.Users.Students;

public record StudentUserDeactivatedMessage() : Message(Guid.NewGuid())
{
    public required Guid UserId { get; init; }
}