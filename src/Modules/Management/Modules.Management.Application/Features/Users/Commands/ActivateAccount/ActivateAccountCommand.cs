using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Commands.ActivateAccount;

/// <summary>
/// Activates <see cref="User"/> account.
/// </summary>
public sealed record ActivateAccountCommand(
    Guid UserId,
    string Code,
    string Password
) : ITransactionCommand<AccessModel>;