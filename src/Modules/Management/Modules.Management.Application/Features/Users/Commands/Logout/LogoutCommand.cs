using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Commands.Logout;

/// <summary>
/// Logs out the <see cref="User"/>.
/// </summary>
public sealed record LogoutCommand : ITransactionCommand;