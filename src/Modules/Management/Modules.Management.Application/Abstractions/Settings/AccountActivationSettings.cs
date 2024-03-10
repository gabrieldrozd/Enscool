using Common.Utilities.Abstractions;

namespace Modules.Management.Application.Abstractions.Settings;

internal sealed class AccountActivationSettings : ISettings
{
    public const string SectionName = "AccountActivationSettings";

    public string BaseUrl { get; set; } = string.Empty;
    public string ActivationLinkFormat { get; set; } = string.Empty;
    public int CodeExpiryInHours { get; set; }
}