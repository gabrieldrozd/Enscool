using Common.Utilities.Abstractions;

namespace Core.Infrastructure.Auth.Settings;

public sealed class AccessSettings : ISettings
{
    public const string SectionName = "AccessSettings";

    public JwtSettings JwtSettings { get; init; } = default!;
    public RefreshTokenSettings RefreshTokenSettings { get; init; } = default!;
}