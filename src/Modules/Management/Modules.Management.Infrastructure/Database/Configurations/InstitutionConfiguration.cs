using Core.Infrastructure.Database.Configurations;
using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Management.Domain.Institutions;

namespace Modules.Management.Infrastructure.Database.Configurations;

internal sealed class InstitutionConfiguration : AggregateConfiguration<Institution>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Institution> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion<InstitutionIdConverter>()
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.State).IsRequired();

        builder.Property(x => x.ShortName);
        builder.Property(x => x.FullName);

        builder.OwnsOne(x => x.Address, ownedBuilder =>
        {
            ownedBuilder.Property(x => x.ZipCode).IsRequired();
            ownedBuilder.Property(x => x.ZipCodeCity).IsRequired();
            ownedBuilder.Property(x => x.City).IsRequired();
            ownedBuilder.Property(x => x.HouseNumber).IsRequired();
            ownedBuilder.Property(x => x.State);
            ownedBuilder.Property(x => x.Street);
        });

        builder.OwnsMany(x => x.AdministratorIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("InstitutionId");
            ownedBuilder.ToTable("InstitutionAdministratorIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("AdministratorId");

            builder.Metadata.FindNavigation(nameof(Institution.AdministratorIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }
}