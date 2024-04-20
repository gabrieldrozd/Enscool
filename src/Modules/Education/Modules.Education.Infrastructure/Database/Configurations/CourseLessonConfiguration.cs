using Core.Infrastructure.Database.Configurations;
using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Education.Domain.Courses;
using Modules.Education.Infrastructure.Database.Converters;

namespace Modules.Education.Infrastructure.Database.Configurations;

internal sealed class CourseLessonConfiguration : EntityConfiguration<CourseLesson>
{
    protected override void ConfigureEntity(EntityTypeBuilder<CourseLesson> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Subject)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();

        builder.Property(x => x.Date)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.Property(x => x.StartTime)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.Property(x => x.EndTime)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.Property(x => x.CourseId)
            .HasConversion<CourseIdConverter>()
            .IsRequired();
    }
}