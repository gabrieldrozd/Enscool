using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Access.ResetPassword;

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