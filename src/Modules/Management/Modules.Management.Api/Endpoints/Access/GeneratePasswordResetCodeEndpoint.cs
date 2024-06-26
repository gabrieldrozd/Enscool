using Common.Utilities.Abstractions.Mapping;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Access.Commands.GeneratePasswordResetCode;

namespace Modules.Management.Api.Endpoints.Access;

/// <summary>
/// Generate password reset code <see cref="EndpointBase"/>.
/// </summary>
internal sealed class GeneratePasswordResetCodeEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "password/reset-code",
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
                    "email": "email@email.com"
                }
                """);
    }
}

internal sealed class GeneratePasswordResetCodeRequest : IWithMapTo<GeneratePasswordResetCodeCommand>
{
    public required string Email { get; init; }

    public GeneratePasswordResetCodeCommand Map() => new(Email);
}