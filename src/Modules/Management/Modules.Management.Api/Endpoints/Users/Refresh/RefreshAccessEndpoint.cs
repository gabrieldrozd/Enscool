using Core.Application.Auth;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Users.Refresh;

/// <summary>
/// Login <see cref="EndpointBase"/>.
/// </summary>
internal sealed class RefreshAccessEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "refresh",
                ManagementEndpointInfo.Access,
                async (RefreshAccessRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .AllowAnonymous()
            .ProducesEnvelope<AccessModel>(StatusCodes.Status200OK)
            .WithDocumentation(
                "RefreshAccess",
                "Refresh Access",
                "Refreshes access token and user data",
                """
                {
                    "userId": "00000000-0000-0000-0000-000000000000",
                    "refreshToken": "00000000-0000-0000-0000-000000000000"
                }
                """);
    }
}