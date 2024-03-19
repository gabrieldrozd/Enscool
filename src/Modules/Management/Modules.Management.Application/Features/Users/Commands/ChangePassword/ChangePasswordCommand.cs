using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Commands.ChangePassword;

/// <summary>
/// Activates <see cref="User"/> account.
/// </summary>
public sealed record ChangePasswordCommand(
    string OldPassword,
    string NewPassword
) : ITransactionCommand;