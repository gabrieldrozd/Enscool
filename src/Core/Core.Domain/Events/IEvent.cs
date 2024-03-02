using MediatR;

namespace Core.Domain.Events;

/// <summary>
/// Marker interface for domain events.
/// </summary>
public interface IEvent : INotification;