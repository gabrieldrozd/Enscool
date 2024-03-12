using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Users.Activate;

/// <summary>
/// Register <see cref="EndpointBase"/>.
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
            .ProducesEnvelope(StatusCodes.Status201Created)
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