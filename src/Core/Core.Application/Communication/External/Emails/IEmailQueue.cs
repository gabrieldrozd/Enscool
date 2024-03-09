namespace Core.Application.Communication.External.Emails;

public interface IEmailQueue
{
    void Enqueue(EmailMessage message);
    bool Dequeue(out EmailMessage? message);
}