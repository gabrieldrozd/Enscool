using Core.Domain.Events;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.Events;

/// <summary>
/// Raised when an institution admin has been registered
/// </summary>
public sealed record InstitutionAdminRegisteredEvent(
    UserId UserId,
    Email Email,
    Phone Phone,
    FullName FullName,
    InstitutionId InstitutionId
) : IEvent;