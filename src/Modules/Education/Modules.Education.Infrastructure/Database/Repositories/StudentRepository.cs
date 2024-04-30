using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Core.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Students;

namespace Modules.Education.Infrastructure.Database.Repositories;

public sealed class StudentRepository : Repository<Student, EducationDbContext>, IStudentRepository
{
    private readonly EducationDbContext _context;

    public StudentRepository(EducationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(Email email, CancellationToken cancellationToken = default)
        => await _context.Students.AnyAsync(x => x.Email == email, cancellationToken);

    public async Task<bool> ExistsWithinInstitutionAsync(Email email, InstitutionId institutionId, CancellationToken cancellationToken = default)
        => await _context.Students.AnyAsync(x => x.Email == email && x.InstitutionId == institutionId, cancellationToken);

    public async Task<bool> ExistAsync(IEnumerable<UserId> userIds, CancellationToken cancellationToken = default)
        => await _context.Students.AnyAsync(x => userIds.Contains(x.Id), cancellationToken);

    public async Task<Student?> GetAsync(UserId userId, CancellationToken cancellationToken = default)
        => await _context.Students.FindAsync([userId], cancellationToken);

    public async Task<Student?> GetAsync(Email email, CancellationToken cancellationToken = default)
        => await _context.Students
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
}
