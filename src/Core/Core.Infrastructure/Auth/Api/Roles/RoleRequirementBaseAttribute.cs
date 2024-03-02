using Core.Domain.Shared.Enumerations.Roles;
using Microsoft.AspNetCore.Authorization;

namespace Core.Infrastructure.Auth.Api.Roles;

/// <summary>
/// <para>Applied when a user must be in a specific role to access a resource.</para>
/// <para>No roles are allowed by default.</para>
/// </summary>
public sealed class RoleRequirementBaseAttribute : AuthorizeAttribute
{
    public new UserRole[] Roles { get; }

    public RoleRequirementBaseAttribute(params UserRole[] roles)
    {
        Roles = roles;
        Policy = $"{PolicyPrefix.Roles}:{string.Join(";", Roles)}";
    }
}