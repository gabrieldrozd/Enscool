using Common.Utilities.Abstractions;

namespace Modules.Management.Application.Abstractions.Settings;

internal sealed class PasswordResetSettings : ISettings
{
    public const string SectionName = "PasswordResetSettings";

    public int CodeExpiryInHours { get; set; }
}