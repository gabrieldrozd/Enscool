using Core.Domain.Events;

namespace Core.Domain.Primitives;

/// <summary>
/// Represents the aggregate root.
/// <typeparam name="TId">
/// The type of the aggregate root identifier which must inherit from <see cref="EntityId"/>.
/// </typeparam>
/// </summary>
public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
    where TId : EntityId
{
    private readonly List<IEvent> _domainEvents = [];

    /// <summary>
    /// Gets the domain events read-only collection.
    /// </summary>
    public IReadOnlyList<IEvent> DomainEvents => _domainEvents.AsReadOnly();

    public uint Version { get; set; } = default;

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected AggregateRoot()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateRoot{TId}"/> class.
    /// </summary>
    /// <param name="id">The aggregate root identifier.</param>
    protected AggregateRoot(TId id)
        : base(id)
    {
    }

    public void Raise(IEvent @event) => _domainEvents.Add(@event);
    public void ClearDomainEvents() => _domainEvents.Clear();
}