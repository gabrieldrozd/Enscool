using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
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

    public async Task<bool> ExistsAsync(Email email, CancellationToken cancellationToken = default)
        => await _context.Users.AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<bool> ExistsInstitutionUserAsync(Email email, CancellationToken cancellationToken = default)
        => await _context.Users
            .AnyAsync(x => x.Email == email && (
                x.Role == UserRole.InstitutionAdmin ||
                x.Role == UserRole.Secretary ||
                x.Role == UserRole.Teacher ||
                x.Role == UserRole.Student
            ), cancellationToken);

    public async Task<bool> ExistsBackOfficeUserAsync(Email email, CancellationToken cancellationToken = default)
        => await _context.Users
            .AnyAsync(x => x.Email == email && (
                x.Role == UserRole.GlobalAdmin ||
                x.Role == UserRole.BackOfficeAdmin ||
                x.Role == UserRole.Support
            ), cancellationToken);

    public async Task<User?> GetAsync(UserId userId, CancellationToken cancellationToken = default)
        => await _context.Users.FindAsync([userId], cancellationToken);

    public async Task<User?> GetAsync(Email email, CancellationToken cancellationToken = default)
        => await _context.Users
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
}