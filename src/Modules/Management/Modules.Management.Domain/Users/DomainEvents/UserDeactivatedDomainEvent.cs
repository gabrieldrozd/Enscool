using Core.Domain.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;

namespace Modules.Management.Domain.Users.DomainEvents;

public sealed record UserDeactivatedDomainEvent(
    UserId UserId,
    UserRole Role
) : IDomainEvent;