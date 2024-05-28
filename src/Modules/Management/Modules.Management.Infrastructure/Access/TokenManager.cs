using Core.Application.Auth;
using Modules.Management.Application.Abstractions.Access;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Access;

internal sealed class TokenManager : ITokenManager
{
    private readonly IUserContext _userContext;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly IBlockedTokenStore _blockedTokenStore;
    private readonly IRefreshTokenStore _refreshTokenStore;

    public TokenManager(
        IUserContext userContext,
        IAccessTokenProvider accessTokenProvider,
        IBlockedTokenStore blockedTokenStore,
        IRefreshTokenStore refreshTokenStore)
    {
        _userContext = userContext;
        _accessTokenProvider = accessTokenProvider;
        _blockedTokenStore = blockedTokenStore;
        _refreshTokenStore = refreshTokenStore;
    }

    public async Task<AccessModel> GenerateAsync(User user)
    {
        var accessToken = _accessTokenProvider.Create(user);
        var refreshToken = _refreshTokenStore.Generate(user.Id);
        await _refreshTokenStore.StoreAsync(refreshToken);

        return new AccessModel
        {
            AccessToken = accessToken.Token,
            ExpiresAt = accessToken.ExpiresAt,
            RefreshToken = refreshToken.Value,
            UserId = user.Id,
            InstitutionId = user.InstitutionId,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        => await _refreshTokenStore.ValidateAsync(userId, refreshToken);

    public async Task RevokeRefreshTokenAsync(Guid userId)
        => await _refreshTokenStore.RevokeAsync(userId);

    public async Task BlockAccessTokenAsync(Guid userId)
    {
        _userContext.EnsureAuthenticated();
        await _blockedTokenStore.BlockAsync(userId, _userContext.Token);
    }
}