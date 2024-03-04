using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;

namespace Core.Domain.Shared.Defaults;

public static class SystemUser
{
    public static readonly UserId Id = UserId.From("00000000-0000-0000-0000-000000000099");
    public const UserRole Role = UserRole.System;
}