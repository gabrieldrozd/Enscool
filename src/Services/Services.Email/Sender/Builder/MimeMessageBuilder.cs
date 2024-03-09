using MimeKit;

namespace Services.Email.Sender.Builder;

public sealed class MimeMessageBuilder : IMimeMessageBuilderWithSender, IWithRecipient, IWithBody, IWithImportance, IWithSubject
{
    private readonly MimeMessage _mimeMessage;

    private MimeMessageBuilder(string fromName, string fromAddress)
    {
        _mimeMessage = new();
        _mimeMessage.From.Add(new MailboxAddress(fromName, fromAddress));
    }

    public static IMimeMessageBuilderWithSender Create(string fromName, string fromAddress)
    {
        return new MimeMessageBuilder(fromName, fromAddress);
    }

    public IWithRecipient WithRecipient(string toName, string toAddress)
    {
        _mimeMessage.To.Add(new MailboxAddress(toName, toAddress));
        return this;
    }

    public IWithBody WithBody(string htmlBody, string textBody)
    {
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = htmlBody,
            TextBody = textBody
        };

        _mimeMessage.Body = bodyBuilder.ToMessageBody();
        return this;
    }

    public IWithImportance WithImportance(MessageImportance importance)
    {
        _mimeMessage.Importance = importance;
        return this;
    }

    public IWithSubject WithSubject(string subject)
    {
        _mimeMessage.Subject = subject;
        return this;
    }

    public MimeMessage Build() => _mimeMessage;
}