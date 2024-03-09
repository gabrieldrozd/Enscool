using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Users.Register;

/// <summary>
/// Register <see cref="EndpointBase"/>.
/// </summary>
public sealed class RegisterEndpoint : EndpointBase
{
    public RegisterEndpoint(ISender sender) : base(sender)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                ManagementEndpointInfo.Users,
                async (RegisterRequest request) =>
                {
                    var result = await Sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .ProducesEnvelope(StatusCodes.Status201Created)
            .WithDocumentation(
                "Register",
                "Register as Institution Admin",
                "Registers new InstitutionAdmin user with new InstitutionId",
                """
                {
                    "Email": "string",
                    "Phone": "string",
                    "FirstName": "string",
                    "MiddleName": "string",
                    "LastName": "string"
                }
                """);
    }
}