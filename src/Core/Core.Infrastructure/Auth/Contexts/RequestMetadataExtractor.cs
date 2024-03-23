using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Core.Infrastructure.Auth.Contexts;

internal static class RequestMetadataExtractor
{
    private const string BearerTokenPrefix = "Bearer ";

    internal static string? GetToken(HttpContext? httpContext)
    {
        var authorization = new StringValues();
        var authorizationHeaderExists = httpContext?.Request.Headers.TryGetValue("Authorization", out authorization);

        var token = authorization.ToString();
        var isBearerToken = token.StartsWith(BearerTokenPrefix, StringComparison.OrdinalIgnoreCase);
        return authorizationHeaderExists.HasValue && authorizationHeaderExists.Value && isBearerToken
            ? token[BearerTokenPrefix.Length..].Trim()
            : null;
    }

    internal static IEnumerable<Claim> DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(token);
        return jsonToken.Claims;
    }
}