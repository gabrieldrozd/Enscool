using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Messaging.Users.Students;

namespace Core.Messaging.Users.Teachers;

public record TeacherUserCreatedMessage(TeacherUserCreatedMessagePayload Payload)
    : Message(Guid.NewGuid());

public sealed class TeacherUserCreatedMessagePayload
{
    public required Guid UserId { get; init; }
    public required UserState State { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string FirstName { get; init; }
    public required string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public required AddressPayload Address { get; init; }
    public required Guid InstitutionId { get; init; }
}