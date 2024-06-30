using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.Payloads;

namespace Core.Messaging.Users.Teachers;

public record TeacherUserUpdatedMessage() : Message(Guid.NewGuid())
{
    public required Guid UserId { get; init; }
    public required string Phone { get; init; }
    public required string FirstName { get; init; }
    public required string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public required AddressPayload Address { get; init; }
}