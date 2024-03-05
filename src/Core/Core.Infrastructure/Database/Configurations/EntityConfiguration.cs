using Core.Domain.Primitives;
using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Database.Configurations;

public abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        ConfigureEntityProperties(builder);
        ConfigureEntity(builder);
    }

    protected abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

    protected void ConfigureEntityProperties(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.InstitutionId)
            .HasConversion<InstitutionIdConverter>()
            .IsRequired(false);

        builder.Property(x => x.CreatedOnUtc)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.Property(x => x.CreatedBy)
            .HasConversion<UserIdConverter>()
            .IsRequired(false);

        builder.Property(x => x.ModifiedOnUtc)
            .HasConversion<DateConverter>()
            .IsRequired(false);

        builder.Property(x => x.ModifiedBy)
            .HasConversion<UserIdConverter>()
            .IsRequired(false);

        builder.Property(x => x.DeletedOnUtc)
            .HasConversion<DateConverter>()
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasConversion<UserIdConverter>()
            .IsRequired(false);

        builder.Property(x => x.Deleted)
            .HasDefaultValue(false);

        builder.HasQueryFilter(x => !x.Deleted);
    }
}