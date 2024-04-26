using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Education.Domain.Courses;

public sealed class Course : AggregateRoot<CourseId>
{
    private readonly List<CourseLesson> _lessons = [];

    private readonly List<UserId> _studentIds = [];
    private readonly List<UserId> _teacherIds = [];

    public CourseCode Code { get; private set; } = null!;
    public LanguageLevel Level { get; private set; } = null!;

    public string? Name { get; private set; }
    public string? Description { get; private set; }

    public Date? PlannedStart { get; private set; }
    public Date? PlannedEnd { get; private set; }

    public IReadOnlyList<CourseLesson> Lessons => _lessons.AsReadOnly();

    public UserId MainTeacherId { get; private set; } = default!;
    public IReadOnlyList<UserId> TeacherIds => _teacherIds.AsReadOnly();
    public IReadOnlyList<UserId> StudentIds => _studentIds.AsReadOnly();

    private Course()
    {
    }

    private Course(
        CourseId courseId,
        CourseCode code,
        LanguageLevel level,
        string? name,
        string? description,
        Date? plannedStart,
        Date? plannedEnd,
        UserId mainTeacherId,
        List<UserId> studentIds,
        List<UserId> teacherIds,
        InstitutionId institutionId
    )
        : base(courseId)
    {
        Code = code;
        Level = level;
        Name = name;
        Description = description;
        PlannedStart = plannedStart;
        PlannedEnd = plannedEnd;
        MainTeacherId = mainTeacherId;

        _studentIds.AddRange(studentIds);
        _teacherIds.AddRange(teacherIds);

        SetInstitutionId(institutionId);
    }

    public static Course Create(
        CourseCode code,
        LanguageLevel level,
        string name,
        string description,
        Date plannedStart,
        Date? plannedEnd,
        UserId mainTeacherId,
        IEnumerable<UserId> studentIds,
        InstitutionId institutionId)
    {
        var course = new Course(
            CourseId.New,
            code,
            level,
            name,
            description,
            plannedStart,
            plannedEnd,
            mainTeacherId,
            studentIds.ToList(),
            [mainTeacherId],
            institutionId);

        return course;
    }
}