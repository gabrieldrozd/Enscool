using Common.Utilities.Models;
using Modules.Management.Application.Features.Users.Commands.Register;

namespace Modules.Management.Api.Endpoints.Users.Register;

internal sealed class RegisterRequest : IWithMapTo<RegisterCommand>
{
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = null!;

    public RegisterCommand Map() => new(Email, Phone, FirstName, MiddleName, LastName);
}