using Core.Domain.Events;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.Events;

/// <summary>
/// Raised when an institution user has been created
/// </summary>
public sealed record InstitutionUserCreatedEvent(
    UserId UserId,
    Email Email,
    Phone Phone,
    FullName FullName,
    UserRole Role,
    Date? BirthDate,
    Address? Address,
    LanguageLevel? LanguageLevel,
    InstitutionId InstitutionId
) : IEvent;