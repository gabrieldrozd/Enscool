using Core.Application.Auth;
using Modules.Management.Application.Abstractions.Access;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Access;

internal sealed class TokenManager : ITokenManager
{
    private readonly IAccessTokenStore _accessTokenStore;
    private readonly IRefreshTokenStore _refreshTokenStore;

    public TokenManager(IAccessTokenStore accessTokenStore, IRefreshTokenStore refreshTokenStore)
    {
        _accessTokenStore = accessTokenStore;
        _refreshTokenStore = refreshTokenStore;
    }

    public async Task<AccessModel> GenerateAsync(User user)
    {
        var accessToken = _accessTokenStore.Create(user);
        var refreshToken = _refreshTokenStore.Generate(user.Id);
        await _refreshTokenStore.StoreAsync(refreshToken);

        return new AccessModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Value,
            UserId = user.Id,
            InstitutionId = user.InstitutionId,
            FirstName = user.FullName.First,
            MiddleName = user.FullName.Middle,
            LastName = user.FullName.Last,
            Email = user.Email,
            Phone = user.Phone,
            State = user.State,
            Role = user.Role
        };
    }

    public async Task<bool> ValidateRefreshTokenAsync(Guid userId, string refreshToken)
        => await _refreshTokenStore.ValidateAsync(userId, refreshToken);

    public async Task RevokeRefreshTokenAsync(Guid userId)
        => await _refreshTokenStore.RevokeAsync(userId);

    // block access token
    public async Task BlockAccessTokenAsync(Guid userId, string accessToken)
        => await _accessTokenStore.BlockAsync(userId, accessToken);
}