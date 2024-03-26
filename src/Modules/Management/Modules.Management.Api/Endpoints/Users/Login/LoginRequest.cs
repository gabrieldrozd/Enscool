using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Users.Commands.Login;

namespace Modules.Management.Api.Endpoints.Users.Login;

internal sealed class LoginRequest : IWithMapTo<LoginCommand>
{
    public required string Email { get; init; }
    public required string Password { get; init; }

    public LoginCommand Map() => new(Email, Password);
}