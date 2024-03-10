using Common.Utilities.Abstractions;

namespace Services.Email;

public sealed class EmailSettings : ISettings
{
    public const string SectionName = "EmailSettings";

    public bool UseSsl { get; set; }
    public int TlsSmtpPort { get; set; } = default!;
    public int SslSmtpPort { get; set; } = default!;
    public string SmtpServer { get; set; } = default!;

    public string FromAddress { get; set; } = default!;
    public string FromName { get; set; } = default!;

    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
}