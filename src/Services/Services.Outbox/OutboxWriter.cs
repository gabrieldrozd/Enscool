using Core.Application.Communication.External.Messages;
using Services.Outbox.OutboxMessages;

namespace Services.Outbox;

internal sealed class OutboxWriter : IOutboxWriter
{
    private readonly OutboxDbContext _context;

    public OutboxWriter(OutboxDbContext context)
    {
        _context = context;
    }

    public async Task<bool> InsertMessageAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage
    {
        var outboxMessage = OutboxMessage.Create(message.MessageId, message.GetType(), message);
        await _context.OutboxMessages.AddAsync(outboxMessage, cancellationToken);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}