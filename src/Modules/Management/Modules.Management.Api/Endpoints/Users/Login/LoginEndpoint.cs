using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Users.Login;

/// <summary>
/// Register <see cref="EndpointBase"/>.
/// </summary>
internal sealed class LoginEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "login",
                ManagementEndpointInfo.Access,
                async (LoginRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .AllowAnonymous()
            .ProducesEnvelope(StatusCodes.Status201Created)
            .WithDocumentation(
                "Login",
                "Login to the system",
                "Logs in to the system with the specified credentials.",
                """
                {
                    "email": "example_email@email.com",
                    "password": "SomePassword123!@#"
                }
                """);
    }
}