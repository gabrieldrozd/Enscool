using Core.Application.Communication.External.Messages;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Core.Messaging.Users;

public record StudentUserCreatedMessage(StudentUserCreatedMessagePayload Payload)
    : Message(Guid.NewGuid());

public sealed class StudentUserCreatedMessagePayload
{
    public required UserId UserId { get; init; }
    public required UserState State { get; init; }
    public required Email Email { get; init; }
    public required Phone Phone { get; init; }
    public required FullName FullName { get; init; }
    public required Date BirthDate { get; init; }
    public required Address Address { get; init; }
    public required LanguageLevel LanguageLevel { get; init; }
    public required InstitutionId InstitutionId { get; init; }
}