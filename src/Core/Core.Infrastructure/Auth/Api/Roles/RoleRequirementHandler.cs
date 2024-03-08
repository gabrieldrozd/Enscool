using Common.Utilities.Exceptions;
using Core.Application.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Core.Infrastructure.Auth.Api.Roles;

public sealed class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    private readonly IUserContext _userContext;

    public RoleRequirementHandler(IUserContext userContext)
    {
        _userContext = userContext;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        RoleRequirement requirement)
    {
        if (_userContext.IsInRole(requirement.Roles))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
            throw new NotAllowedException();
        }

        return Task.CompletedTask;
    }
}