namespace Services.Email.Sender.Builder;

public interface IWithRecipient
{
    IWithBody WithBody(string htmlBody, string textBody);
}