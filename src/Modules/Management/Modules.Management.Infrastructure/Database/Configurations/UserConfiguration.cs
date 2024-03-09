using Core.Infrastructure.Database.Configurations;
using Core.Infrastructure.Database.Converters;
using Microsoft.EntityFrameworkCore;
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

        builder.OwnsMany(x => x.ActivationCodes, activationCode =>
        {
            activationCode.ToTable("UserActivationCodes");
            activationCode.Property(x => x.Value)
                .IsRequired();

            activationCode.Property(x => x.Expires)
                .HasConversion<DateConverter>()
                .IsRequired();

            activationCode.Property(x => x.IsActive)
                .IsRequired();

            activationCode.Property(x => x.CreatedAt)
                .HasConversion<DateConverter>()
                .IsRequired();

            builder.Metadata.FindNavigation(nameof(User.ActivationCodes))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        #region Indexes

        builder.HasIndex(x => x.Email).IsUnique();

        #endregion
    }
}