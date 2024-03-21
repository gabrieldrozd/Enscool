using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Commands.ResetPassword;

/// <summary>
/// Reset <see cref="User"/> password.
/// </summary>
public sealed record ResetPasswordCommand(string Email, string Code, string NewPassword) : ITransactionCommand;