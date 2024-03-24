using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Application.Auth;
using Core.Domain.Shared.ValueObjects;
using Core.Infrastructure.Auth.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Modules.Management.Application.Abstractions.Access;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Access;

internal sealed class AccessTokenProvider : IAccessTokenProvider
{
    private readonly JwtSettings _jwtSettings;

    public AccessTokenProvider(IOptions<AccessSettings> settings)
    {
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
            new Claim(ClaimConsts.InstitutionIds, /* TODO: Add user.InstitutionIds */ string.Empty),
            new Claim(ClaimConsts.FullName, user.FullName.ToString()),
            new Claim(ClaimConsts.Email, user.Email.Value),
            new Claim(ClaimConsts.Phone, user.Phone.Value),
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
}