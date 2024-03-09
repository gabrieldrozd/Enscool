namespace Services.Email.Sender.Builder;

public interface IMimeMessageBuilderWithSender
{
    IWithRecipient WithRecipient(string toName, string toAddress);
}