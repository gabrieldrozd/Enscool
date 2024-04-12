using Core.Domain.Shared.ValueObjects;

namespace Services.Outbox.OutboxMessages;

public sealed class OutboxMessage
{
    public Guid Id { get; }
    public string Type { get; } = null!;
    public object Payload { get; } = null!;
    public MessageState State { get; private set; }
    public Date CreatedOnUtc { get; } = Date.UtcNow;
    public Date? ProcessedOnUtc { get; private set; }
    public string? Error { get; private set; }

    private OutboxMessage()
    {
    }

    private OutboxMessage(Guid id, string type, object payload)
    {
        Id = id;
        Type = type;
        Payload = payload;
        State = MessageState.Pending;
        CreatedOnUtc = Date.UtcNow;
        ProcessedOnUtc = null;
        Error = null;
    }

    public static OutboxMessage Create(Guid id, Type type, object payload)
        => new(id, type.FullName!, payload);

    public void SetProcessed()
    {
        State = MessageState.Processed;
        ProcessedOnUtc = Date.UtcNow;
    }

    public void SetFailed(string error)
    {
        State = MessageState.Failed;
        ProcessedOnUtc = Date.UtcNow;
        Error = error;
    }
}