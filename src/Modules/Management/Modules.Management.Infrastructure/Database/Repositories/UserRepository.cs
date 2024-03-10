using Core.Domain.Shared.ValueObjects;
using Core.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Database.Repositories;

public sealed class UserRepository : Repository<User, ManagementDbContext>, IUserRepository
{
    private readonly ManagementDbContext _context;

    public UserRepository(ManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default)
        => _context.Users.AnyAsync(x => x.Email == email, cancellationToken);
}