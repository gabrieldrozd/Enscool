using Common.Utilities.Abstractions.Mapping;
using Core.Application.Auth;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Access.Commands.ActivateAccount;

namespace Modules.Management.Api.Endpoints.Access;

/// <summary>
/// Activate <see cref="EndpointBase"/>.
/// </summary>
internal sealed class ActivateAccountEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPatchEndpoint(
                "activate",
                ManagementEndpointInfo.Access,
                async (ActivateAccountRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .AllowAnonymous()
            .ProducesEnvelope<AccessModel>(StatusCodes.Status201Created)
            .WithDocumentation(
                "Activate",
                "Activate User",
                "Activates user account",
                """
                {
                    "userId": "00000000-0000-0000-0000-000000000000",
                    "code": "NIVnT1IJCglFs4KkmcPdmW2sjKCge/d4WMuZYAJLqwAP2zo/FgqmEjUOFvrARibTvu74MT0/sAHPq0Av3gqTGw==",
                    "password": "SomePassword123!@#"
                }
                """);
    }
}

internal sealed class ActivateAccountRequest : IWithMapTo<ActivateAccountCommand>
{
    public required Guid UserId { get; init; }
    public required string Code { get; init; }
    public required string Password { get; init; }

    public ActivateAccountCommand Map() => new(UserId, Code, Password);
}