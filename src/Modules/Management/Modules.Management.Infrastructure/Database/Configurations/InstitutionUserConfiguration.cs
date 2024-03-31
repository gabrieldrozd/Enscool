using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Database.Configurations;

internal sealed class InstitutionUserConfiguration : IEntityTypeConfiguration<InstitutionUser>
{
    public void Configure(EntityTypeBuilder<InstitutionUser> builder)
    {
        builder.Property(x => x.BirthDate)
            .HasConversion<DateConverter>();

        builder.OwnsOne(x => x.Address, ownedBuilder =>
        {
            ownedBuilder.Property(x => x.ZipCode).IsRequired();
            ownedBuilder.Property(x => x.ZipCodeCity).IsRequired();
            ownedBuilder.Property(x => x.State);
            ownedBuilder.Property(x => x.City).IsRequired();
            ownedBuilder.Property(x => x.Street);
            ownedBuilder.Property(x => x.HouseNumber).IsRequired();
        });

        builder.Property(x => x.LanguageLevel)
            .HasConversion<LanguageLevelConverter>()
            .IsRequired();
    }
}