using Core.Domain.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.DomainEvents;

/// <summary>
/// Raised when an institution user has been created
/// </summary>
public sealed record InstitutionUserCreatedDomainEvent(
    UserId UserId,
    UserState State,
    Email Email,
    Phone Phone,
    string FirstName,
    string? MiddleName,
    string LastName,
    UserRole Role,
    Date? BirthDate,
    Address? Address,
    LanguageLevel? LanguageLevel,
    InstitutionId InstitutionId
) : IDomainEvent;