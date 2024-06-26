using Core.Application.Communication.External.Messages;

namespace Core.Messaging.Users.Students;

public record StudentUserDeletedMessage(StudentUserDeletedMessagePayload Payload)
    : Message(Guid.NewGuid());

public sealed class StudentUserDeletedMessagePayload
{
    public required Guid UserId { get; init; }
}