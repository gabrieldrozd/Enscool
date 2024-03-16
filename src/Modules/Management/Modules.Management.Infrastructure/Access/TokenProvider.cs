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

namespace Modules.Management.Infrastructure.Access;

internal sealed class TokenProvider : ITokenProvider
{
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSettings _jwtSettings;

    public TokenProvider(IOptions<AccessSettings> settings)
    {
        var key = settings.Value.JwtSettings.IssuerSigningKey;
        _signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            SecurityAlgorithms.HmacSha256);

        _jwtSettings = settings.Value.JwtSettings;
    }

    public AccessModel Create(User user)
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
            signingCredentials: _signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        var accessToken = new AccessModel
        {
            AccessToken = tokenValue,
            Expires = expires.ToUnixSeconds(),
            UserId = user.Id.Value,
            InstitutionId = user.InstitutionId?.Value,
            FirstName = user.FullName.First,
            MiddleName = user.FullName.Middle,
            LastName = user.FullName.Last,
            Email = user.Email.Value,
            State = user.State,
            Role = user.Role
        };

        return accessToken;
    }
}