using Core.Infrastructure.Database.Configurations;
using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Education.Domain.Teachers;

namespace Modules.Education.Infrastructure.Database.Configurations;

internal sealed class TeacherConfiguration : AggregateConfiguration<Teacher>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion<UserIdConverter>()
            .ValueGeneratedNever()
            .IsRequired();

        builder.OwnsMany(x => x.CourseIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("TeacherId");
            ownedBuilder.ToTable("TeacherCourseIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("CourseId");

            builder.Metadata.FindNavigation(nameof(Teacher.CourseIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}