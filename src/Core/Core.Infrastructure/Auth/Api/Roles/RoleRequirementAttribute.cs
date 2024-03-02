using Core.Domain.Shared.Enumerations.Roles;
using Microsoft.AspNetCore.Authorization;

namespace Core.Infrastructure.Auth.Api.Roles;

/// <summary>
/// <para>Applied when a user must be in a specific role to access a resource.</para>
/// <para><see cref="UserRole.GlobalAdmin"/> and <see cref="UserRole.BackOfficeAdmin"/> are always allowed.</para>
/// </summary>
public sealed class RoleRequirementAttribute : AuthorizeAttribute
{
    public new UserRole[] Roles { get; }

    public RoleRequirementAttribute(params UserRole[] roles)
    {
        Roles = [UserRole.GlobalAdmin, UserRole.BackOfficeAdmin, ..roles];
        Policy = $"{PolicyPrefix.Roles}:{string.Join(";", Roles)}";
    }
}