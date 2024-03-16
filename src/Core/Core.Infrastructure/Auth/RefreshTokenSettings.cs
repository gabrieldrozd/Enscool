using Common.Utilities.Abstractions;

namespace Core.Infrastructure.Auth;

public class RefreshTokenSettings : ISettings
{
    public int ExpiryInHours { get; set; }
    public int Length { get; set; }
}