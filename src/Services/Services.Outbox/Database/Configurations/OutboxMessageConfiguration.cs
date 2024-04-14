using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Services.Outbox.OutboxMessages;

namespace Services.Outbox.Database.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        var contractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };
        var settings = new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            TypeNameHandling = TypeNameHandling.All,
            NullValueHandling = NullValueHandling.Include
        };
        settings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });

        builder.Property(x => x.Payload)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonConvert.SerializeObject(v, settings),
                v => JsonConvert.DeserializeObject<object>(v, settings)!);

        builder.Property(x => x.State);

        builder.Property(x => x.CreatedOnUtc)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.Property(x => x.ProcessedOnUtc)
            .HasConversion<DateConverter>();
    }
}