using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.InstitutionUsers.Create;

/// <summary>
/// Register <see cref="EndpointBase"/>.
/// </summary>
internal sealed class CreateInstitutionUserEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                ManagementEndpointInfo.InstitutionUsers,
                async (CreateInstitutionUserRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .AllowAnonymous()
            .ProducesEnvelope(StatusCodes.Status201Created)
            .WithDocumentation(
                "CreateInstitutionUser",
                "Create new Institution user",
                "Creates new Institution user with provided data",
                """
                {
                    "email": "example_email@email.com",
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
                    },
                    "languageLevel": 3,
                    "role": 0,
                    "institutionId": "00000000-0000-0000-0000-000000000000"
                }
                """);
    }
}