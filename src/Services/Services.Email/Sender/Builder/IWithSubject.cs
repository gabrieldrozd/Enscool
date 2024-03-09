using MimeKit;

namespace Services.Email.Sender.Builder;

public interface IWithSubject
{
    MimeMessage Build();
}