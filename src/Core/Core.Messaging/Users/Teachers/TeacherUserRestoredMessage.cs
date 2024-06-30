using Core.Application.Communication.External.Messages;

namespace Core.Messaging.Users.Teachers;

public record TeacherUserRestoredMessage() : Message(Guid.NewGuid())
{
    public required Guid UserId { get; init; }
}