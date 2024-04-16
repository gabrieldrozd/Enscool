using Core.Domain.Primitives;

namespace Modules.Education.Domain.Courses;

public sealed class Course : AggregateRoot<CourseId>
{
    public CourseCode Code { get; set; }

    private Course()
    {
    }

    private Course(CourseId courseId, CourseCode code)
        : base(courseId)
    {
        Code = code;
    }

    public static Course Create(CourseId courseId, CourseCode code)
        => new(courseId, code);
}