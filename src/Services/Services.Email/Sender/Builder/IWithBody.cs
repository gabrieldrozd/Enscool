using MimeKit;

namespace Services.Email.Sender.Builder;

public interface IWithBody
{
    IWithImportance WithImportance(MessageImportance importance);
}