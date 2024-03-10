using Common.Utilities.Abstractions;

namespace Core.Infrastructure.Auth;

public sealed class AuthenticationSettings : ISettings
{
    public const string SectionName = "AuthenticationSettings";

    public JwtSettings JwtSettings { get; init; } = default!;
}