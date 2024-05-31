using Common.Utilities.Abstractions;

namespace Core.Infrastructure.Auth.Settings;

public class JwtSettings : ISettings
{
    public required string IssuerSigningKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int ExpiryInSeconds { get; set; }
}