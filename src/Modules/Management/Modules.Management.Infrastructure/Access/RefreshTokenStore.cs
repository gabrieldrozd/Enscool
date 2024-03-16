using System.Security.Cryptography;
using System.Text.Json;
using Core.Application.Auth;
using Core.Domain.Shared.EntityIds;
using Core.Infrastructure.Auth;
using Microsoft.Extensions.Options;
using Modules.Management.Application.Abstractions.Access;
using StackExchange.Redis;

namespace Modules.Management.Infrastructure.Access;

internal sealed class RefreshTokenStore : IRefreshTokenStore
{
    private const string RefreshTokenKey = "RefreshToken:{0}";

    private readonly IDatabase _redisDatabase;
    private readonly RefreshTokenSettings _settings;

    public RefreshTokenStore(
        IConnectionMultiplexer connectionMultiplexer,
        IOptions<AccessSettings> settings)
    {
        _redisDatabase = connectionMultiplexer.GetDatabase();
        _settings = settings.Value.RefreshTokenSettings;
    }

    public RefreshToken Generate(UserId userId)
    {
        var tokenValue = GenerateRandomTokenString();
        return RefreshToken.Create(userId, tokenValue, _settings.ExpiryInHours);
    }

    public async Task StoreAsync(RefreshToken refreshToken)
    {
        var key = string.Format(RefreshTokenKey, refreshToken.UserId);
        var value = JsonSerializer.Serialize(refreshToken);
        var expiry = refreshToken.Expires - refreshToken.Created;
        await _redisDatabase.StringSetAsync(key, value, expiry);
    }

    public async Task<bool> ValidateAsync(Guid userId, string token)
    {
        var key = string.Format(RefreshTokenKey, userId);
        var value = await _redisDatabase.StringGetAsync(key);

        if (value.IsNullOrEmpty)
            return false;

        var storedToken = JsonSerializer.Deserialize<RefreshToken>(value.ToString());
        return storedToken is not null && storedToken.Value == token && !storedToken.IsExpired;
    }

    public async Task RevokeAsync(Guid userId)
    {
        var key = string.Format(RefreshTokenKey, userId);
        await _redisDatabase.KeyDeleteAsync(key);
    }

    private string GenerateRandomTokenString()
    {
        var randomToken = new byte[_settings.Length];
        RandomNumberGenerator.Create().GetBytes(randomToken);
        return Convert.ToBase64String(randomToken);
    }
}