using Core.Domain.Primitives;

namespace Core.Domain.Shared.EntityIds;

/// <summary>
/// Represents a user identifier.
/// </summary>
public sealed class UserId : EntityId
{
    private UserId(Guid value) : base(value)
    {
    }

    public static UserId? Empty => null;
    public static UserId New => new(Guid.NewGuid());
    public static UserId From(Guid id) => new(id);
    public static UserId? From(Guid? id) => id is null ? null : new UserId(id.Value);
    public static UserId From(string id) => new(Guid.Parse(id));
    public static IEnumerable<UserId> From(IEnumerable<Guid> ids) => ids.Select(From);

    public static implicit operator UserId(Guid id) => From(id);
    public static implicit operator UserId?(Guid? id) => From(id);
    public static implicit operator Guid(UserId id) => id.Value;
    public static implicit operator Guid?(UserId? id) => id?.Value;
    public static implicit operator string(UserId id) => id.Value.ToString();
}