using MediatR;

namespace Core.Domain.DomainEvents;

/// <summary>
/// Marker interface for domain events.
/// </summary>
public interface IDomainEvent : INotification;