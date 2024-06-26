using Core.Application.Communication.External.Messages;

namespace Core.Messaging.Users.Teachers;

public record TeacherUserDeletedMessage(TeacherUserDeletedMessagePayload Payload)
    : Message(Guid.NewGuid());

public sealed class TeacherUserDeletedMessagePayload
{
    public required Guid UserId { get; init; }
}