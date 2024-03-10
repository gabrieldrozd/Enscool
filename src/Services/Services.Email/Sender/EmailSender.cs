using Common.Utilities.Extensions;
using Core.Application.Communication.External.Emails;
using Core.Application.Helpers;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using Services.Email.Sender.Builder;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Services.Email.Sender;

public sealed class EmailSender : IEmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task Send(EmailMessage? emailMessage, CancellationToken cancellationToken)
    {
        if (emailMessage is null || TestDetector.IsTestMode())
            return;

        try
        {
            using var message = MimeMessageBuilder.Create(_emailSettings.FromName, _emailSettings.FromAddress)
                .WithRecipient(emailMessage.Recipient.ToName, emailMessage.Recipient.ToAddress)
                .WithBody(emailMessage.HtmlBody, emailMessage.TextBody)
                .WithImportance(MessageImportance.High)
                .WithSubject(emailMessage.Subject)
                .Build();

            using var client = new SmtpClient();

            // TODO: Test whether it works as expected
            // TODO: Test whether it works as expected
            // TODO: Test whether it works as expected
            await _emailSettings.UseSsl.IfElse(
                ifTrue: client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SslSmtpPort, SecureSocketOptions.SslOnConnect, cancellationToken),
                ifFalse: client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.TlsSmtpPort, SecureSocketOptions.StartTls, cancellationToken));

            // await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password, cancellationToken);

            _logger.LogInformation("[EmailService]: Sending email to '{ToAddress}'", emailMessage.Recipient.ToAddress);
            await client.SendAsync(message, cancellationToken);
            _logger.LogInformation("[EmailService]: Email sent successfully to '{ToAddress}'", emailMessage.Recipient.ToAddress);
        }
        catch (Exception e)
        {
            _logger.LogError("[EmailService]: Error sending email to '{ToAddress}'", emailMessage.Recipient.ToAddress);
            _logger.LogError("{Exception}", e);
        }
    }
}