using Common.Utilities.Emails;
using Common.Utilities.Exceptions;

namespace Core.Application.Communication.External.Emails;

public sealed class EmailMessage
{
    public EmailRecipient Recipient { get; private set; }
    public string Subject { get; private set; }
    public string HtmlBody { get; private set; }
    public string TextBody { get; private set; }

    private EmailMessage(EmailRecipient recipient, string subject, string htmlBody, string textBody)
    {
        Recipient = recipient;
        Subject = subject;
        HtmlBody = htmlBody;
        TextBody = textBody;
    }

    /// <summary>
    /// Creates <see cref="EmailMessage"/> from an <see cref="EmailTemplate"/>.
    /// </summary>
    /// <param name="toAddress">Email address of the recipient.</param>
    /// <param name="toName">Name of the recipient.</param>
    /// <param name="emailTemplate">Email template with the subject and properties to render.</param>
    /// <returns>Email message.</returns>
    /// <exception cref="ApplicationLayerException">Thrown when the email template fails to render.</exception>
    public static EmailMessage Create(string toAddress, string toName, EmailTemplate emailTemplate)
        => emailTemplate.Render(out var subject, out var htmlBody, out var textBody)
            ? new EmailMessage(EmailRecipient.Create(toAddress, toName), subject, htmlBody, textBody)
            : throw new ApplicationLayerException("Failed to render email template.");
}