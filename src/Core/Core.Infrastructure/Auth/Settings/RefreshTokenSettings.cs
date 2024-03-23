using Common.Utilities.Abstractions;

namespace Core.Infrastructure.Auth.Settings;

public class RefreshTokenSettings : ISettings
{
    public int ExpiryInMinutes { get; set; }
    public int Length { get; set; }
}