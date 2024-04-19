using Core.Domain.Primitives;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Education.Domain.Courses;

public sealed class CourseLesson : Entity<Guid>
{
    public string Subject { get; private set; }
    public string Description { get; private set; }

    public Date Date { get; set; }
    // TODO: Start time
    // TODO: End time
    // TODO: lesson materials - information about the files uploaded to some provider
    // TODO: lesson attendance - information about the students who attended the lesson

    public CourseId CourseId { get; private set; }
}