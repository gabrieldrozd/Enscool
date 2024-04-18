using Core.Infrastructure.Database.Configurations;
using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Education.Domain.Courses;
using Modules.Education.Infrastructure.Database.Converters;

namespace Modules.Education.Infrastructure.Database.Configurations;

internal sealed class CourseConfiguration : AggregateConfiguration<Course>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion<CourseIdConverter>()
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Code)
            .HasConversion<CourseCodeConverter>()
            .IsRequired();

        builder.Property(x => x.Level)
            .HasConversion<LanguageLevelConverter>()
            .IsRequired();

        builder.Property(x => x.Name);
        builder.Property(x => x.Description);

        builder.Property(x => x.PlannedStart)
            .HasConversion<DateConverter>();

        builder.Property(x => x.PlannedEnd)
            .HasConversion<DateConverter>();

        builder.Property(x => x.MainTeacherId)
            .HasConversion<UserIdConverter>()
            .IsRequired();

        builder.OwnsMany(x => x.StudentIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("CourseId");
            ownedBuilder.ToTable("CourseStudentIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("StudentId");

            builder.Metadata.FindNavigation(nameof(Course.StudentIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.OwnsMany(x => x.TeacherIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("CourseId");
            ownedBuilder.ToTable("CourseTeacherIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("TeacherId");

            builder.Metadata.FindNavigation(nameof(Course.TeacherIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        #region Indexes

        builder.HasIndex(x => x.Code).IsUnique();

        #endregion
    }
}