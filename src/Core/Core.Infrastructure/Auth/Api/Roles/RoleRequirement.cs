using Core.Domain.Shared.Enumerations.Roles;
using Microsoft.AspNetCore.Authorization;

namespace Core.Infrastructure.Auth.Api.Roles;

public sealed class RoleRequirement : IAuthorizationRequirement
{
    public UserRole[] Roles { get; }

    public RoleRequirement(string roles)
    {
        if (string.IsNullOrWhiteSpace(roles))
        {
            Roles = Array.Empty<UserRole>();
            return;
        }

        Roles = roles.Contains(';')
            ? roles.Split(';').Select(Enum.Parse<UserRole>).ToArray()
            : [Enum.Parse<UserRole>(roles)];
    }
}