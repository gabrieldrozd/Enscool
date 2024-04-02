using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Commands.Login;

/// <summary>
/// Logs in the <see cref="User"/>.
/// </summary>
public sealed record LoginCommand(
    string Email,
    string Password
) : ITransactionCommand<AccessModel>;