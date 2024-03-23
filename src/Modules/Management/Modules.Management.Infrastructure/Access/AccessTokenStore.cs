using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Application.Auth;
using Core.Domain.Shared.ValueObjects;
using Core.Infrastructure.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modules.Management.Application.Abstractions.Access;
using Modules.Management.Domain.Users;
using StackExchange.Redis;

namespace Modules.Management.Infrastructure.Access;

internal sealed class AccessTokenStore : IAccessTokenStore
{
    private const string BlockedTokensKey = "BlockedTokens";

    private readonly IDatabase _redisDatabase;
    private readonly JwtSettings _jwtSettings;

    public AccessTokenStore(IConnectionMultiplexer connectionMultiplexer, IOptions<AccessSettings> settings)
    {
        _redisDatabase = connectionMultiplexer.GetDatabase();
        _jwtSettings = settings.Value.JwtSettings;
    }

    public string Create(User user)
    {
        var expires = Date.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes);

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimConsts.UserId, user.Id.ToString()),
            new Claim(ClaimConsts.InstitutionId, user.InstitutionId?.ToString() ?? string.Empty),
            new Claim(ClaimConsts.FullName, user.FullName.ToString()),
            new Claim(ClaimConsts.Email, user.Email.Value),
            new Claim(ClaimConsts.UserState, $"{(int) user.State}"),
            new Claim(ClaimConsts.UserRole, $"{(int) user.Role}")
        ];

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: expires.DateTime,
            signingCredentials: new SigningCredentials(
                key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey)),
                algorithm: SecurityAlgorithms.HmacSha256));

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public async Task BlockAsync(Guid userId, string accessToken)
        => await _redisDatabase.SetAddAsync(BlockedTokensKey, accessToken);

    public async Task<bool> IsBlockedAsync(Guid userId, string accessToken)
        => await _redisDatabase.SetContainsAsync(BlockedTokensKey, accessToken);
}