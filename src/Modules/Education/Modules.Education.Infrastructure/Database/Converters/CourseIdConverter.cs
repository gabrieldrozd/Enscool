using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Education.Domain.Courses;

namespace Modules.Education.Infrastructure.Database.Converters;

public sealed class CourseIdConverter : ValueConverter<CourseId, Guid>
{
    public CourseIdConverter() : base(
        courseId => courseId.Value,
        courseId => CourseId.From(courseId)
    )
    {
    }
}