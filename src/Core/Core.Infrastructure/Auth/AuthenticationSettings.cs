namespace Core.Infrastructure.Auth;

public sealed class AuthenticationSettings
{
    public const string SectionName = "AuthenticationSettings";

    public JwtSettings JwtSettings { get; init; } = default!;
}