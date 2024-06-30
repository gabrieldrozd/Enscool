using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.Payloads;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Commands.UpdateInstitutionUser;

namespace Modules.Management.Api.Endpoints.InstitutionUsers;

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

internal sealed class UpdateInstitutionUserRequest : IWithMapTo<UpdateInstitutionUserCommand>
{
    public string Phone { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = null!;
    public DateTime? BirthDate { get; init; }
    public AddressPayload? Address { get; init; }

    public UpdateInstitutionUserCommand Map() => new()
    {
        Phone = Phone,
        FirstName = FirstName,
        MiddleName = MiddleName,
        LastName = LastName,
        BirthDate = BirthDate,
        Address = Address
    };
}