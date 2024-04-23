using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Education.Domain.Courses;

public sealed class CourseLesson : Entity<Guid>
{
    public string Subject { get; private set; } = null!;
    public string Description { get; private set; } = null!;

    public Date Date { get; private set; } = null!;

    public Date StartTime { get; private set; } = null!;
    public Date EndTime { get; private set; } = null!;

    public CourseId CourseId { get; private set; } = null!;

    private CourseLesson()
    {
    }

    private CourseLesson(Guid courseLessonId, string subject, string description, Date date, Date startTime, Date endTime, CourseId courseId, InstitutionId institutionId)
        : base(courseLessonId)
    {
        Subject = subject;
        Description = description;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        CourseId = courseId;

        SetInstitutionId(institutionId);
    }

    public static CourseLesson Create(string subject, string description, Date date, Date startTime, Date endTime, CourseId courseId, InstitutionId institutionId)
        => new(Guid.NewGuid(), subject, description, date, startTime, endTime, courseId, institutionId);
}