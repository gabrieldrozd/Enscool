using Core.Application.Communication.External.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Communication.External.Messages;

internal sealed class MessageBus : IMessageBus
{
    private readonly IServiceProvider _serviceProvider;

    public MessageBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage
    {
        using var scope = _serviceProvider.CreateScope();
        var _outboxWriter = scope.ServiceProvider.GetRequiredService<IOutboxWriter>();

        var inserted = await _outboxWriter.InsertMessageAsync(message, cancellationToken);
        if (!inserted) throw new InvalidOperationException($"Failed to insert '{typeof(TMessage).FullName}' message into outbox");
    }
}