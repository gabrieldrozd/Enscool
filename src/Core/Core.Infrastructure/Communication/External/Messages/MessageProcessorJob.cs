using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Communication.External.Messages;

internal sealed class MessageProcessorJob : BackgroundService
{
    private const string RequestType = "MessageProcessor";

    private readonly InMemoryMessageQueue _queue;
    private readonly IPublisher _publisher;
    private readonly ILogger<MessageProcessorJob> _logger;

    public MessageProcessorJob(InMemoryMessageQueue queue, IPublisher publisher, ILogger<MessageProcessorJob> logger)
    {
        _queue = queue;
        _publisher = publisher;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in _queue.Reader.ReadAllAsync(stoppingToken))
        {
            _logger.LogInformation("[{@Timestamp} | {@RequestType}]: Processing '{@Request}' message",
                DateTime.UtcNow.ToString("s"),
                RequestType,
                message.GetType().Name);

            await _publisher.Publish(message, stoppingToken);

            _logger.LogInformation("[{@Timestamp} | {@RequestType}]: Completed processing '{@Request}' message",
                DateTime.UtcNow.ToString("s"),
                RequestType,
                message.GetType().Name);
        }
    }
}