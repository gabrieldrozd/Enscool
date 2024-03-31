using Core.Domain.DomainEvents;

namespace Core.Domain.Primitives;

public interface IAggregateRoot : IEntity
{
    /// <summary>
    /// Gets the domain events read-only collection.
    /// </summary>
    IReadOnlyList<IDomainEvent> DomainEvents { get; }

    uint Version { get; }

    void RaiseDomainEvent(IDomainEvent domainEvent);

    void ClearDomainEvents();
}