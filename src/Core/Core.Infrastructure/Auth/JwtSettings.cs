namespace Core.Infrastructure.Auth;

public class JwtSettings
{
    public required string IssuerSigningKey { get; set; }
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required int ExpiryInMinutes { get; set; }
}