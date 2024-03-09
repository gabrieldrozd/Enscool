using Core.Application.Communication.External.Emails;

namespace Services.Email.Sender;

public interface IEmailSender
{
    public Task Send(EmailMessage? emailMessage, CancellationToken cancellationToken);
}