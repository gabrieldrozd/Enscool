using Microsoft.EntityFrameworkCore;
using Modules.Management.Domain.Institutions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions;

public interface IManagementDbContext
{
    DbSet<Institution> Institutions { get; }
    DbSet<User> Users { get; }
}