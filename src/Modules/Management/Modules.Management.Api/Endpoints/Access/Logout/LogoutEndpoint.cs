using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Users.Commands.Logout;

namespace Modules.Management.Api.Endpoints.Access.Logout;

/// <summary>
/// Logout <see cref="EndpointBase"/>.
/// </summary>
internal sealed class LogoutEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "logout",
                ManagementEndpointInfo.Access,
                async (ISender sender) =>
                {
                    var result = await sender.Send(new LogoutCommand());
                    return BuildEnvelope(result);
                })
            .AllowAnonymous()
            .ProducesEnvelope(StatusCodes.Status200OK)
            .WithDocumentation(
                "Logout",
                "Logout from the system",
                "Logs out from the system.");
    }
}