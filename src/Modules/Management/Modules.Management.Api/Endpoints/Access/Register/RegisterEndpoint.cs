using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Access.Register;

/// <summary>
/// Register <see cref="EndpointBase"/>.
/// </summary>
internal sealed class RegisterEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "register",
                ManagementEndpointInfo.Access,
                async (RegisterRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .AllowAnonymous()
            .ProducesEnvelope(StatusCodes.Status201Created)
            .WithDocumentation(
                "Register",
                "Register as Institution Admin",
                "Registers new InstitutionAdmin user with new InstitutionId",
                """
                {
                    "email": "example_email@email.com",
                    "phone": "+48512456456",
                    "firstName": "John",
                    "middleName": null,
                    "lastName": "Doe"
                }
                """);
    }
}