using Core.Application.Communication.External.Messages;

namespace Core.Messaging.Users.Students;

public record StudentUserRestoredMessage(StudentUserRestoredMessagePayload Payload)
    : Message(Guid.NewGuid());

public sealed class StudentUserRestoredMessagePayload
{
    public required Guid UserId { get; init; }
}