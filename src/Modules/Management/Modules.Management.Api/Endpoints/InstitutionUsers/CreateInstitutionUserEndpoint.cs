using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Payloads;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Commands.CreateInstitutionUser;

namespace Modules.Management.Api.Endpoints.InstitutionUsers;

/// <summary>
/// Create user <see cref="EndpointBase"/>.
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

internal sealed class CreateInstitutionUserRequest : IWithMapTo<CreateInstitutionUserCommand>
{
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = null!;
    public DateTime? BirthDate { get; init; }
    public AddressPayload? Address { get; init; }
    public int? LanguageLevel { get; init; }
    public InstitutionUserRole Role { get; init; }
    public Guid InstitutionId { get; init; }

    public CreateInstitutionUserCommand Map() => new()
    {
        Email = Email,
        Phone = Phone,
        FirstName = FirstName,
        MiddleName = MiddleName,
        LastName = LastName,
        BirthDate = BirthDate,
        Address = Address,
        LanguageLevel = LanguageLevel,
        Role = Role,
        InstitutionId = InstitutionId
    };
}