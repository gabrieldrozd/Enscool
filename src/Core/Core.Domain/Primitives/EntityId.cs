namespace Core.Domain.Primitives;

/// <summary>
/// Base class for <see cref="AggregateRoot{TId}" /> identifiers.
/// </summary>
public abstract class EntityId : ValueObject, IEquatable<EntityId>
{
    public Guid Value { get; }

    protected EntityId(Guid value) => Value = value;

    public static implicit operator Guid(EntityId id) => id.Value;
    public static implicit operator string(EntityId id) => id.Value.ToString();

    public override string ToString() => Value.ToString();

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public bool Equals(EntityId? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((EntityId) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Value);
    }

    public static bool operator ==(EntityId? left, EntityId? right)
        => left?.Value == right?.Value;

    public static bool operator !=(EntityId? left, EntityId? right)
        => !(left == right);
}