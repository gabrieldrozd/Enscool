using Core.Application.Auth;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions.Access;

public interface IAccessTokenStore
{
    /// <summary>Creates a new <see cref="AccessModel"/> for the given <see cref="User"/>.</summary>
    string Create(User user);

    /// <summary>Checks if the access token is blocked for the given <see cref="User"/>.</summary>
    Task<bool> IsBlockedAsync(Guid userId, string accessToken);

    /// <summary>Blocks the access token for the given <see cref="User"/>.</summary>
    Task BlockAsync(Guid userId, string accessToken);
}