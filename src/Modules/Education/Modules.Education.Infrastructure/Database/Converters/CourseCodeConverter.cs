using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Education.Domain.Courses;

namespace Modules.Education.Infrastructure.Database.Converters;

public sealed class CourseCodeConverter : ValueConverter<CourseCode, string>
{
    public CourseCodeConverter() : base(
        courseCode => courseCode.Value,
        courseCode => CourseCode.From(courseCode)
    )
    {
    }
}