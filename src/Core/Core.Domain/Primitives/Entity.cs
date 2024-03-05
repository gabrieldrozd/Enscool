using Core.Domain.Primitives.Rules;
using Core.Domain.Shared.Defaults;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Rules;
using Core.Domain.Shared.ValueObjects;

namespace Core.Domain.Primitives;

public abstract class Entity<TId> : IEquatable<Entity<TId>>, IEntity
{
    /// <summary>
    /// The entity identifier.
    /// </summary>
    public TId Id { get; } = default!;

    /// <summary>
    /// The related institution identifier.
    /// </summary>
    public InstitutionId? InstitutionId { get; private set; }

    /// <summary>
    /// Created on date and time in UTC format.
    /// </summary>
    public Date CreatedOnUtc { get; private set; } = Date.UtcNow;

    /// <summary>
    /// Created by UserId.
    /// </summary>
    public UserId? CreatedBy { get; private set; }

    /// <summary>
    /// Modified on date and time in UTC format.
    /// <seealso cref="Date"/>
    /// </summary>
    public Date? ModifiedOnUtc { get; private set; }

    /// <summary>
    /// Modified by UserId.
    /// </summary>
    public UserId? ModifiedBy { get; private set; }

    /// <summary>
    /// Deleted on date and time in UTC format.
    /// </summary>
    public Date? DeletedOnUtc { get; private set; }

    /// <summary>
    /// Deleted by UserId.
    /// </summary>
    public UserId? DeletedBy { get; private set; }

    /// <summary>
    /// Indicates whether the entity is deleted.
    /// </summary>
    public bool Deleted { get; private set; }

    /// <summary>
    /// Indicates whether the entity can be restored.
    /// </summary>
    public bool CanBeRestored => Deleted && DeletedOnUtc is not null && Date.UtcNow <= DeletedOnUtc.AddDays(30);

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Entity()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(TId id)
    {
        Id = id;
    }

    public void SetCreated(Date createdOnUtc, UserId? createdBy) => (CreatedOnUtc, CreatedBy) = (createdOnUtc, createdBy ?? SystemUser.Id);
    public void SetModified(Date modifiedOnUtc, UserId? modifiedBy) => (ModifiedOnUtc, ModifiedBy) = (modifiedOnUtc, modifiedBy ?? SystemUser.Id);
    public void SetDeletedBy(UserId? deletedBy) => DeletedBy = deletedBy;

    public virtual void Delete()
    {
        Validate(new CanBeDeletedRule(this));

        Deleted = true;
        DeletedOnUtc = Date.UtcNow;
    }

    public virtual void Restore()
    {
        Validate(new CanBeRestoredRule(this));

        Deleted = false;
        DeletedOnUtc = null;
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
        => left is not null && right is not null && left.Equals(right);

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
        => !(left == right);

    public bool Equals(Entity<TId>? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (Id is null || other.Id is null)
            return false;

        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (GetType() != obj.GetType())
            return false;

        if (Id is null || ((Entity<TId>) obj).Id is null)
            return false;

        return Id.Equals(((Entity<TId>) obj).Id);
    }

    public override int GetHashCode()
        => (GetType(), Id).GetHashCode();

    protected void SetInstitutionId(InstitutionId? institutionId) => InstitutionId = institutionId;

    /// <summary>
    /// Validates the business rule.
    /// </summary>
    /// <param name="rule">The business rule.</param>
    /// <exception cref="BusinessRuleException">Thrown when the business rule is not valid.</exception>
    protected static void Validate(IBusinessRule rule)
    {
        if (rule.IsInvalid())
        {
            throw new BusinessRuleException(rule);
        }
    }
}