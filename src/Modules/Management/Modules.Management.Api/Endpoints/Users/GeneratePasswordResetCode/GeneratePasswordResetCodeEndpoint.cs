using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Users.GeneratePasswordResetCode;

/// <summary>
/// Generate password reset code <see cref="EndpointBase"/>.
/// </summary>
internal sealed class GeneratePasswordResetCodeEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "generate-password-reset-code",
                ManagementEndpointInfo.Access,
                async (GeneratePasswordResetCodeRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .AllowAnonymous()
            .ProducesEnvelope(StatusCodes.Status200OK)
            .WithDocumentation(
                "GeneratePasswordResetCode",
                "Generate password reset code",
                "Generates a password reset code for the user with the specified email.",
                """
                {
                    "email": "email@email.com
                }
                """);
    }
}