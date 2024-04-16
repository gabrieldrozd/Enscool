using Core.Domain.Primitives;

namespace Modules.Education.Domain.Courses;

public class CourseId : EntityId
{
    private CourseId(Guid value) : base(value)
    {
    }

    public static CourseId? Empty => null;
    public static CourseId New => new(Guid.NewGuid());
    public static CourseId From(Guid id) => new(id);
    public static CourseId? From(Guid? id) => id is null ? null : new CourseId(id.Value);
    public static CourseId From(string id) => new(Guid.Parse(id));
    public static IEnumerable<CourseId> From(IEnumerable<Guid> ids) => ids.Select(From);

    public static implicit operator CourseId(Guid id) => From(id);
    public static implicit operator CourseId?(Guid? id) => From(id);
    public static implicit operator Guid(CourseId id) => id.Value;
    public static implicit operator Guid?(CourseId? id) => id?.Value;
    public static implicit operator string(CourseId id) => id.Value.ToString();
}