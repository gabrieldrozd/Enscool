using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.InstitutionUsers.Update;

/// <summary>
/// Register <see cref="EndpointBase"/>.
/// </summary>
internal sealed class UpdateInstitutionUserEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPutEndpoint(
                "{userId:guid}",
                ManagementEndpointInfo.InstitutionUsers,
                async (Guid userId, [FromBody] UpdateInstitutionUserRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map() with { UserId = userId });
                    return BuildEnvelope(result);
                })
            .RequireInstitutionRoles()
            .ProducesEnvelope(StatusCodes.Status200OK)
            .WithDocumentation(
                "UpdateInstitutionUser",
                "Update Institution user",
                "Updates Institution user with provided data",
                """
                {
                    "phone": "+48512456456",
                    "firstName": "John",
                    "middleName": null,
                    "lastName": "Doe",
                    "birthDate": "1990-01-01",
                    "address": 
                    {
                        "zipCode": "00-000",
                        "zipCodeCity": "Example city",
                        "city": "Example city",
                        "houseNumber": "1",
                        "state": "Example state",
                        "street": "Example street"
                    }
                }
                """);
    }
}