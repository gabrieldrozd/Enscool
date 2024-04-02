using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Access.Commands.Register;

namespace Modules.Management.Api.Endpoints.Access.Register;

internal sealed class RegisterRequest : IWithMapTo<RegisterCommand>
{
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }

    public RegisterCommand Map() => new(Email, Phone, FirstName, MiddleName, LastName);
}