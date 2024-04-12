namespace Services.Outbox.OutboxMessages;

public enum MessageState
{
    Pending = 0,
    Processed = 1,
    Failed = 2
}