namespace Services.Email.Settings;

public sealed class EmailSettings
{
    public const string SectionName = "EmailSettings";

    public bool UseSsl { get; set; }
    public string SmtpServer { get; set; } = default!;
    public int SmtpPort { get; set; } = default!;
    public string FromAddress { get; set; } = default!;
    public string FromName { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string AppPassword { get; set; } = default!;
}