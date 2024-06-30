using Core.Domain.Communication.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;

namespace Modules.Management.Domain.Users.DomainEvents;

/// <summary>
/// Raised when a user has been restored
/// </summary>
public sealed record UserRestoredDomainEvent(
    UserId UserId,
    UserRole Role
) : IDomainEvent;