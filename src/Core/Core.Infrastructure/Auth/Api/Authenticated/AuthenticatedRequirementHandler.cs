using Common.Utilities.Exceptions;
using Core.Application.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Core.Infrastructure.Auth.Api.Authenticated;

public sealed class AuthenticatedRequirementHandler : AuthorizationHandler<AuthenticatedRequirement>
{
    private readonly IUserContext _userContext;

    public AuthenticatedRequirementHandler(IUserContext userContext)
    {
        _userContext = userContext;
    }

    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        AuthenticatedRequirement requirement)
    {
        if (_userContext.Authenticated)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
            throw new NotAuthenticatedException();
        }

        return Task.CompletedTask;
    }
}