using Common.Utilities.Abstractions.Mapping;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Access.Commands.ChangePassword;

namespace Modules.Management.Api.Endpoints.Access;

/// <summary>
/// Change password <see cref="EndpointBase"/>.
/// </summary>
internal sealed class ChangePasswordEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "password/change",
                ManagementEndpointInfo.Access,
                async (ChangePasswordRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .RequireAuthorization()
            .ProducesEnvelope(StatusCodes.Status200OK)
            .WithDocumentation(
                "ChangePassword",
                "Change the password of the user",
                "Changes the password of the user with the specified credentials.",
                """
                {
                    "oldPassword": "OldPassword123!@#",
                    "newPassword": "NewPassword123!@#"
                }
                """);
    }
}

internal sealed class ChangePasswordRequest : IWithMapTo<ChangePasswordCommand>
{
    public required string OldPassword { get; init; }
    public required string NewPassword { get; init; }

    public ChangePasswordCommand Map() => new(OldPassword, NewPassword);
}