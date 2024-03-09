namespace Modules.Management.Application.Abstractions.Settings;

public sealed class AccountActivationSettings
{
    public const string SectionName = "AccountActivationSettings";

    public string BaseUrl { get; set; } = string.Empty;
    public string ActivationLinkFormat { get; set; } = string.Empty;
    public int CodeExpiryInHours { get; set; }
}