using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Database.Configurations;

internal sealed class BackOfficeUserConfiguration : IEntityTypeConfiguration<BackOfficeUser>
{
    public void Configure(EntityTypeBuilder<BackOfficeUser> builder)
    {
    }
}