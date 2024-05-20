using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Commands.RefreshAccess;

/// <summary>
/// Refreshes access token and <see cref="User"/> data.
/// </summary>
/// <param name="UserId"></param>
/// <param name="RefreshToken"></param>
public sealed record RefreshAccessCommand(Guid UserId, string RefreshToken)
    : ITransactionCommand<AccessModel>;