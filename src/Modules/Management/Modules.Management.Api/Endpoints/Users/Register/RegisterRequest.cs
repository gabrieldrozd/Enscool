using Common.Utilities.Models;
using Modules.Management.Application.Features.Users.Commands.Register;

namespace Modules.Management.Api.Endpoints.Users.Register;

internal sealed class RegisterRequest : IWithMapTo<RegisterCommand>
{
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string FirstName { get; init; }
    public string? MiddleName { get; init; }
    public required string LastName { get; init; }

    public RegisterCommand Map() => new(Email, Phone, FirstName, MiddleName, LastName);
}