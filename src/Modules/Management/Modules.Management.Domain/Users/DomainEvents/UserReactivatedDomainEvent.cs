using Core.Domain.DomainEvents;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;

namespace Modules.Management.Domain.Users.DomainEvents;

public sealed record UserReactivatedDomainEvent(
    UserId UserId,
    UserRole Role
) : IDomainEvent;