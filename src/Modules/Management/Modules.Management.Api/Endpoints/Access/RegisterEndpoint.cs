using Common.Utilities.Abstractions.Mapping;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Access.Commands.Register;

namespace Modules.Management.Api.Endpoints.Access;

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

internal sealed class RegisterRequest : IWithMapTo<RegisterCommand>
{
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }

    public RegisterCommand Map() => new(Email, Phone, FirstName, MiddleName, LastName);
}