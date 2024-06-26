using Core.Application.Communication.External.Messages;

namespace Core.Messaging.Users.Teachers;

public record TeacherUserRestoredMessage(TeacherUserRestoredMessagePayload Payload)
    : Message(Guid.NewGuid());

public sealed class TeacherUserRestoredMessagePayload
{
    public required Guid UserId { get; init; }
}