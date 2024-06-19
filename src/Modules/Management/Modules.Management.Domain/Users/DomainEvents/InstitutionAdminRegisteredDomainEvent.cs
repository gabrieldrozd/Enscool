using Core.Domain.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.DomainEvents;

/// <summary>
/// Raised when an institution admin has been registered
/// </summary>
public sealed record InstitutionAdminRegisteredDomainEvent(
    UserId UserId,
    Email Email,
    Phone Phone,
    string FirstName,
    string? MiddleName,
    string LastName,
    InstitutionId InstitutionId
) : IDomainEvent;