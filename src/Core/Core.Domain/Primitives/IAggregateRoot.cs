using Core.Domain.Events;

namespace Core.Domain.Primitives;

public interface IAggregateRoot : IEntity
{
    /// <summary>
    /// Gets the domain events read-only collection.
    /// </summary>
    IReadOnlyList<IEvent> DomainEvents { get; }

    uint Version { get; }

    void Raise(IEvent @event);

    void ClearDomainEvents();
}