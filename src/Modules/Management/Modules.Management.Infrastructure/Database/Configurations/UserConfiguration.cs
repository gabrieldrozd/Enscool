using Core.Infrastructure.Database.Configurations;
using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Management.Domain.Users;
using Modules.Management.Infrastructure.Database.Converters;

namespace Modules.Management.Infrastructure.Database.Configurations;

internal sealed class UserConfiguration : AggregateConfiguration<User>
{
    protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion<UserIdConverter>()
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.State).IsRequired();
        builder.Property(x => x.Email)
            .HasConversion<EmailConverter>()
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasConversion<PhoneConverter>()
            .IsRequired();

        builder.Property(x => x.Password)
            .HasConversion<PasswordConverter>();

        builder.Property(x => x.FullName)
            .HasConversion<FullNameConverter>()
            .IsRequired();

        builder.Property(x => x.Role).IsRequired();

        #region Indexes

        builder.HasIndex(x => x.Email).IsUnique();

        #endregion
    }
}