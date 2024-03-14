using Microsoft.EntityFrameworkCore;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions;

public interface IManagementDbContext
{
    DbSet<User> Users { get; }
}