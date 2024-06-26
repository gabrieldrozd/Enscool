using Common.Utilities.Abstractions.Mapping;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Access.Commands.ResetPassword;

namespace Modules.Management.Api.Endpoints.Access;

/// <summary>
/// Register <see cref="EndpointBase"/>.
/// </summary>
internal sealed class ResetPasswordEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "password/reset",
                ManagementEndpointInfo.Access,
                async (ResetPasswordRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .AllowAnonymous()
            .ProducesEnvelope(StatusCodes.Status200OK)
            .WithDocumentation(
                "ResetPassword",
                "Reset Password",
                "Resets the password.",
                """
                {
                    "email": "example_email@email.com",
                    "code": "1234-5678",
                    "newPassword": "newPassword123!"
                }
                """);
    }
}

internal sealed class ResetPasswordRequest : IWithMapTo<ResetPasswordCommand>
{
    public required string Email { get; init; }
    public required string Code { get; init; }
    public required string NewPassword { get; init; }

    public ResetPasswordCommand Map() => new(Email, Code, NewPassword);
}