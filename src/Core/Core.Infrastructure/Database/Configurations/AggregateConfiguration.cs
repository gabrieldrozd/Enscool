using Core.Domain.Primitives;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Database.Configurations;

public abstract class AggregateConfiguration<TAggregate> : EntityConfiguration<TAggregate>
    where TAggregate : class, IAggregateRoot
{
    public override void Configure(EntityTypeBuilder<TAggregate> builder)
    {
        ConfigureEntityProperties(builder);
        ConfigureAggregate(builder);
        ConfigureEntity(builder);
    }

    private void ConfigureAggregate(EntityTypeBuilder<TAggregate> builder)
    {
        builder.Property(p => p.Version)
            .IsRowVersion();
    }
}