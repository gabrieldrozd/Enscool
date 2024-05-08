using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Modules.Education.Domain.Courses;

namespace Modules.Education.Domain.Teachers;

public sealed class Teacher : AggregateRoot<UserId>
{
    private readonly List<CourseId> _courseIds = [];

    public IReadOnlyList<CourseId> CourseIds => _courseIds;

    private Teacher()
    {
    }

    private Teacher(UserId teacherId)
        : base(teacherId)
    {
    }

    public static Teacher Create(UserId teacherId) => new(teacherId);
}