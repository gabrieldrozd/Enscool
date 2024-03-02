using Core.Infrastructure.Auth.Api.Authenticated;
using Core.Infrastructure.Auth.Api.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Core.Infrastructure.Auth.Api;

internal sealed class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        return policy ?? CreateRolePolicy(policyName);
    }

    private static AuthorizationPolicy CreateRolePolicy(string policyName)
    {
        var roles = policyName.Replace($"{PolicyPrefix.Roles}:", string.Empty);
        return new AuthorizationPolicyBuilder()
            .AddRequirements(
                new RoleRequirement(roles),
                new AuthenticatedRequirement())
            .Build();
    }
}