using Core.Domain.Primitives;

namespace Modules.Education.Domain.Courses;

/// <summary>
/// Represents a course code - a simple, unique identifier for a course.
/// </summary>
public sealed class CourseCode : ValueObject
{
    public string Value { get; }

    private CourseCode(string value)
    {
        Value = value;
    }

    public static CourseCode From(string value) => new(value);

    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}