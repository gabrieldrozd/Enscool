using Core.Domain.Shared.EntityIds;

namespace Core.Application.Auth;

public interface IBlockedTokenStore
{
    /// <summary>Checks if the access token is blocked for the given <see cref="UserId"/>.</summary>
    Task<bool> IsBlockedAsync(Guid userId, string accessToken);

    /// <summary>Blocks the access token for the given <see cref="UserId"/>.</summary>
    Task BlockAsync(Guid userId, string accessToken);
}