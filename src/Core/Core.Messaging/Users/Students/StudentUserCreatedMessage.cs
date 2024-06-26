using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.Enumerations.UserStates;

namespace Core.Messaging.Users.Students;

public record StudentUserCreatedMessage(StudentUserCreatedMessagePayload Payload)
    : Message(Guid.NewGuid());

public sealed class StudentUserCreatedMessagePayload
{
    public required Guid UserId { get; init; }
    public required UserState State { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string FirstName { get; init; }
    public required string? MiddleName { get; init; }
    public required string LastName { get; init; }
    public required DateTime BirthDate { get; init; }
    public required AddressPayload Address { get; init; }
    public required string LanguageLevel { get; init; }
    public required Guid InstitutionId { get; init; }
}

public sealed class AddressPayload
{
    public required string ZipCode { get; init; }
    public required string ZipCodeCity { get; init; }
    public required string City { get; init; }
    public required string HouseNumber { get; init; }
    public required string? State { get; init; }
    public required string? Street { get; init; }
}