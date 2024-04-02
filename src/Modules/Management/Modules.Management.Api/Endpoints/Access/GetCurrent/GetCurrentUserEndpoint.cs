using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Access.Queries.GetCurrentUser;

namespace Modules.Management.Api.Endpoints.Access.GetCurrent;

/// <summary>
/// Get current user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class GetCurrentUserEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                ManagementEndpointInfo.Access,
                async (ISender sender) =>
                {
                    var result = await sender.Send(new GetCurrentUserQuery());
                    return BuildEnvelope(result);
                })
            .RequireAuthorization()
            .ProducesEnvelope<UserDto>(StatusCodes.Status200OK)
            .WithDocumentation(
                "GetCurrentUser",
                "Get current user details",
                "Gets details of currently logged in user.");
    }
}