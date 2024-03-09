using Core.Application.Communication.External.Emails;
using Microsoft.Extensions.Hosting;
using Services.Email.Sender;

namespace Services.Email;

public sealed class EmailSenderBackgroundService : BackgroundService
{
    private readonly IEmailQueue _emailQueue;
    private readonly IEmailSender _emailSender;

    public EmailSenderBackgroundService(IEmailQueue emailQueue, IEmailSender emailSender)
    {
        _emailQueue = emailQueue;
        _emailSender = emailSender;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_emailQueue.Dequeue(out var message))
                await _emailSender.Send(message, stoppingToken);

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}