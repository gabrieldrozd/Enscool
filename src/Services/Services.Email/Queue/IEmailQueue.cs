using Core.Application.Communication.External.Emails;

namespace Services.Email.Queue;

public interface IEmailQueue
{
    void Enqueue(EmailMessage message);
    bool Dequeue(out EmailMessage? message);
}