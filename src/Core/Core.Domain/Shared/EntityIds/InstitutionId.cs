using Core.Domain.Primitives;

namespace Core.Domain.Shared.EntityIds;

public sealed class InstitutionId : EntityId
{
    private InstitutionId(Guid value) : base(value)
    {
    }

    public static InstitutionId? Empty => null;
    public static InstitutionId New => new(Guid.NewGuid());
    public static InstitutionId From(Guid id) => new(id);
    public static InstitutionId? From(Guid? id) => id is null ? null : new InstitutionId(id.Value);
    public static InstitutionId From(string id) => new(Guid.Parse(id));
    public static IEnumerable<InstitutionId> From(IEnumerable<Guid> ids) => ids.Select(From);

    public static implicit operator InstitutionId(Guid value) => From(value);
    public static implicit operator InstitutionId?(Guid? value) => From(value);
    public static implicit operator Guid(InstitutionId id) => id.Value;
    public static implicit operator Guid?(InstitutionId? id) => id?.Value;
    public static implicit operator string(InstitutionId value) => value.Value.ToString();
}