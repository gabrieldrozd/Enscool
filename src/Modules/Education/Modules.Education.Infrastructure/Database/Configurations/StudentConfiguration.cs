using Core.Infrastructure.Database.Configurations;
using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Education.Domain.Students;

namespace Modules.Education.Infrastructure.Database.Configurations;

internal sealed class StudentConfiguration : AggregateConfiguration<Student>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion<UserIdConverter>()
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.State)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion<EmailConverter>()
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasConversion<PhoneConverter>()
            .IsRequired();

        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.MiddleName);
        builder.Property(x => x.LastName).IsRequired();

        builder.Property(x => x.BirthDate)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.OwnsOne(x => x.Address, ownedBuilder =>
            {
                ownedBuilder.Property(x => x.ZipCode).IsRequired();
                ownedBuilder.Property(x => x.ZipCodeCity).IsRequired();
                ownedBuilder.Property(x => x.City).IsRequired();
                ownedBuilder.Property(x => x.HouseNumber).IsRequired();
                ownedBuilder.Property(x => x.State);
                ownedBuilder.Property(x => x.Street);
            })
            .Navigation(x => x.Address)
            .IsRequired();

        builder.Property(x => x.LanguageLevel)
            .HasConversion<LanguageLevelConverter>()
            .IsRequired();

        #region Indexes

        builder.HasIndex(x => x.Email).IsUnique();

        #endregion
    }
}