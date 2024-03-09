using System.Collections.Concurrent;
using Core.Application.Communication.External.Emails;

namespace Services.Email.Queue;

internal sealed class EmailQueue : IEmailQueue
{
    private readonly ConcurrentQueue<EmailMessage?> _queue = new();

    public void Enqueue(EmailMessage message) => _queue.Enqueue(message);

    public bool Dequeue(out EmailMessage? message)
    {
        if (!_queue.IsEmpty)
        {
            return _queue.TryDequeue(out message);
        }

        message = null;
        return false;
    }
}