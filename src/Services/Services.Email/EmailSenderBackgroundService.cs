using Core.Application.Communication.External.Emails;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Email.Sender;

namespace Services.Email;

public sealed class EmailSenderBackgroundService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEmailQueue _emailQueue;

    public EmailSenderBackgroundService(IServiceProvider serviceProvider, IEmailQueue emailQueue)
    {
        _serviceProvider = serviceProvider;
        _emailQueue = emailQueue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_emailQueue.Dequeue(out var message))
            {
                var emailSender = _serviceProvider.GetRequiredService<IEmailSender>();
                await emailSender.Send(message, stoppingToken);
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}