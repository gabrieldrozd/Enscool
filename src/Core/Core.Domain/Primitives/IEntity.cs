using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;

namespace Core.Domain.Primitives;

public interface IEntity
{
    /// <summary>
    /// The related tenant identifier.
    /// </summary>
    InstitutionId? TenantId { get; }

    /// <summary>
    /// Created on date and time in UTC format.
    /// </summary>
    Date CreatedOnUtc { get; }

    /// <summary>
    /// Created by UserId.
    /// </summary>
    public UserId? CreatedBy { get; }

    /// <summary>
    /// Modified on date and time in UTC format.
    /// <seealso cref="Date"/>
    /// </summary>
    Date? ModifiedOnUtc { get; }

    /// <summary>
    /// Modified by UserId.
    /// </summary>
    public UserId? ModifiedBy { get; }

    /// <summary>
    /// Deleted on date and time in UTC format.
    /// </summary>
    Date? DeletedOnUtc { get; }

    /// <summary>
    /// Deleted by UserId.
    /// </summary>
    public UserId? DeletedBy { get; }

    /// <summary>
    /// Indicates whether the entity is deleted.
    /// </summary>
    bool Deleted { get; }

    /// <summary>
    /// Indicates whether the entity can be restored.
    /// </summary>
    bool CanBeRestored { get; }

    /// <summary>
    /// Sets the created on date and time in UTC format.
    /// </summary>
    void SetCreated(Date createdOnUtc, UserId createdBy);

    /// <summary>
    /// Sets the modified on date and time in UTC format.
    /// </summary>
    void SetModified(Date modifiedOnUtc, UserId modifiedBy);

    /// <summary>
    /// Sets the deleted by <see cref="UserId"/>.
    /// </summary>
    void SetDeletedBy(UserId deletedBy);

    /// <summary>
    /// Sets the deleted to <see langword="true"/> and sets the deleted on date and time in UTC format.
    /// </summary>
    void Delete();

    /// <summary>
    /// Sets the deleted to <see langword="false"/> and sets the deleted on date and time in UTC format to <see langword="null"/>.
    /// </summary>
    void Restore();
}