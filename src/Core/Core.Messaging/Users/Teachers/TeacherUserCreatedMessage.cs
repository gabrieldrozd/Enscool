using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.Payloads;

namespace Core.Messaging.Users.Teachers;

public record TeacherUserCreatedMessage() : Message(Guid.NewGuid())
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