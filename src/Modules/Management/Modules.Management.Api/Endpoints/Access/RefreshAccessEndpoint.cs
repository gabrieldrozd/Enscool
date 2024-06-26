using Common.Utilities.Abstractions.Mapping;
using Core.Application.Auth;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Access.Commands.RefreshAccess;

namespace Modules.Management.Api.Endpoints.Access;

/// <summary>
/// Refresh access <see cref="EndpointBase"/>.
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
                    "userId": "8816c6f6-6eed-448c-8ebe-43c2a31849cf",
                    "refreshToken": "8GAT9NI5vSyv7ZYBJfUptKrAxsHXUJ9id7Exejl3+iw="
                }
                """);
    }
}

internal sealed class RefreshAccessRequest : IWithMapTo<RefreshAccessCommand>
{
    public required Guid UserId { get; init; }
    public required string RefreshToken { get; init; }

    public RefreshAccessCommand Map() => new(UserId, RefreshToken);
}