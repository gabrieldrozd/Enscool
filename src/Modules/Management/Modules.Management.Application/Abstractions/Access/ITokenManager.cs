using Core.Application.Auth;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions.Access;

public interface ITokenManager
{
    Task<AccessModel> GenerateAsync(User user);
    Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
    Task RevokeRefreshTokenAsync(Guid userId);
    Task BlockAccessTokenAsync(Guid userId);
}