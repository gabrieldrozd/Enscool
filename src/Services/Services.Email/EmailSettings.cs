using Common.Utilities.Abstractions;

namespace Services.Email;

public sealed class EmailSettings : ISettings
{
    public const string SectionName = "EmailSettings";

    public int SmtpPort { get; init; } = default!;
    public string SmtpServer { get; init; } = default!;

    public string FromAddress { get; init; } = default!;
    public string FromName { get; init; } = default!;

    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
}