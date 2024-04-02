using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Access.Commands.Login;

namespace Modules.Management.Api.Endpoints.Access.Login;

internal sealed class LoginRequest : IWithMapTo<LoginCommand>
{
    public required string Email { get; init; }
    public required string Password { get; init; }

    public LoginCommand Map() => new(Email, Password);
}