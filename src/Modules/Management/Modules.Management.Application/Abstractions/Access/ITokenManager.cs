using Core.Application.Auth;
using Core.Domain.Shared.EntityIds;

namespace Modules.Management.Application.Abstractions.Access;

public interface ITokenManager
{
    RefreshToken GenerateRefreshToken(UserId userId);
    Task StoreRefreshToken(RefreshToken refreshToken);
    Task<bool> ValidateRefreshToken(Guid userId, string token);
    Task RevokeRefreshTokenAsync(Guid userId);
}