using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Communication.External.Messages;

internal sealed class MessageProcessorJob : BackgroundService
{
    private const string RequestType = "MessageProcessor";

    private readonly IServiceProvider _serviceProvider;
    private readonly InMemoryMessageQueue _queue;
    private readonly ILogger<MessageProcessorJob> _logger;

    public MessageProcessorJob(IServiceProvider serviceProvider, InMemoryMessageQueue queue, ILogger<MessageProcessorJob> logger)
    {
        _serviceProvider = serviceProvider;
        _queue = queue;
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

            using var scope = _serviceProvider.CreateScope();
            var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
            await publisher.Publish(message, stoppingToken);

            _logger.LogInformation("[{@Timestamp} | {@RequestType}]: Completed processing '{@Request}' message",
                DateTime.UtcNow.ToString("s"),
                RequestType,
                message.GetType().Name);
        }
    }
}