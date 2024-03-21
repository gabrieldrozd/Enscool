using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Commands.ChangePassword;

/// <summary>
/// Changes <see cref="User"/> password.
/// </summary>
public sealed record ChangePasswordCommand(
    string OldPassword,
    string NewPassword
) : ITransactionCommand;