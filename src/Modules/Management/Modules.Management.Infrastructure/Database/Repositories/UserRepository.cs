using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;
using Core.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Database.Repositories;

internal sealed class UserRepository : Repository<User, ManagementDbContext>, IUserRepository
{
    private readonly ManagementDbContext _context;

    public UserRepository(ManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<bool> DoesInstitutionUserExistAsync(Email email, CancellationToken cancellationToken = default)
        => _context.Users
            .Where(x =>
                x.Role == UserRole.Student ||
                x.Role == UserRole.Teacher ||
                x.Role == UserRole.Secretary ||
                x.Role == UserRole.InstitutionAdmin)
            .AnyAsync(x => x.Email == email, cancellationToken);
}