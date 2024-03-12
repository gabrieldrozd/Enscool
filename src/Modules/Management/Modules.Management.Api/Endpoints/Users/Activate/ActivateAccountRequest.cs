using Common.Utilities.Models;
using Modules.Management.Application.Features.Users.Commands.ActivateAccount;

namespace Modules.Management.Api.Endpoints.Users.Activate;

internal sealed class ActivateAccountRequest : IWithMapTo<ActivateAccountCommand>
{
    public Guid UserId { get; init; }
    public string Code { get; init; } = null!;
    public string Password { get; init; } = null!;

    public ActivateAccountCommand Map() => new(UserId, Code, Password);
}