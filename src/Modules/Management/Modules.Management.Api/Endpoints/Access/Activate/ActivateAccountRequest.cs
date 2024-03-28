using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Users.Commands.ActivateAccount;

namespace Modules.Management.Api.Endpoints.Access.Activate;

internal sealed class ActivateAccountRequest : IWithMapTo<ActivateAccountCommand>
{
    public required Guid UserId { get; init; }
    public required string Code { get; init; }
    public required string Password { get; init; }

    public ActivateAccountCommand Map() => new(UserId, Code, Password);
}