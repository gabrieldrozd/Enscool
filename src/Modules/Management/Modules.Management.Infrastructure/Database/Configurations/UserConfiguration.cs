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

        builder.Property(x => x.FirstName).IsRequired();
        builder.Property(x => x.MiddleName);
        builder.Property(x => x.LastName).IsRequired();

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

        builder.OwnsMany(x => x.PasswordResetCodes, resetCode =>
        {
            resetCode.ToTable("UserPasswordResetCodes");
            resetCode.Property(x => x.Value)
                .IsRequired();

            resetCode.Property(x => x.Expires)
                .HasConversion<DateConverter>()
                .IsRequired();

            resetCode.Property(x => x.IsActive)
                .IsRequired();

            resetCode.Property(x => x.CreatedAt)
                .HasConversion<DateConverter>()
                .IsRequired();

            builder.Metadata.FindNavigation(nameof(User.PasswordResetCodes))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.HasDiscriminator<string>("Type")
            .HasValue<InstitutionUser>(nameof(InstitutionUser))
            .HasValue<BackOfficeUser>(nameof(BackOfficeUser));

        builder.OwnsMany(x => x.InstitutionIds, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("UserId");
            ownedBuilder.ToTable("BackOfficeUserInstitutionIds");
            ownedBuilder.HasKey("Id");

            ownedBuilder.Property(x => x.Value)
                .ValueGeneratedNever()
                .HasColumnName("InstitutionId");

            builder.Metadata.FindNavigation(nameof(BackOfficeUser.InstitutionIds))!
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        #region Indexes

        builder.HasIndex(x => x.Email).IsUnique();

        #endregion
    }
}