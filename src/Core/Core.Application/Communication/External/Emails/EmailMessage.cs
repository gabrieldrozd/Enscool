using Common.Utilities.Emails;
using Common.Utilities.Exceptions;

namespace Core.Application.Communication.External.Emails;

public sealed class EmailMessage
{
    public EmailRecipient Recipient { get; private set; }
    public string Subject { get; private set; }
    public string HtmlBody { get; private set; }
    public string TextBody { get; private set; }

    public EmailMessage(EmailRecipient recipient, string subject, string htmlBody, string textBody)
    {
        Recipient = recipient;
        Subject = subject;
        HtmlBody = htmlBody;
        TextBody = textBody;
    }

    /// <summary>
    /// Creates <see cref="EmailMessage"/> from an <see cref="EmailTemplate"/>.
    /// </summary>
    /// <param name="recipient">Email recipient (email address and name).</param>
    /// <param name="emailTemplate">Email template with the subject and properties to render.</param>
    /// <returns>Email message.</returns>
    /// <exception cref="ApplicationLayerException">Thrown when the email template fails to render.</exception>
    public static EmailMessage Create(EmailRecipient recipient, EmailTemplate emailTemplate)
        => emailTemplate.Render(out var subject, out var htmlBody, out var textBody)
            ? new EmailMessage(recipient, subject, htmlBody, textBody)
            : throw new ApplicationLayerException("Failed to render email template.");
}