using MediatR;

namespace Core.Domain.Events;

/// <summary>
/// Represents a domain event handler interface.
/// </summary>
/// <typeparam name="TDomainEvent">The domain event type.</typeparam>
public interface IEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IEvent;