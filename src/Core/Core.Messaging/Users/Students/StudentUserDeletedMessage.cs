using Core.Application.Communication.External.Messages;

namespace Core.Messaging.Users.Students;

public record StudentUserDeletedMessage() : Message(Guid.NewGuid())
{
    public required Guid UserId { get; init; }
}