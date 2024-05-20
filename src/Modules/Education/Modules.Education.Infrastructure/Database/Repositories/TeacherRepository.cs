using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Core.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Teachers;

namespace Modules.Education.Infrastructure.Database.Repositories;

public sealed class TeacherRepository : Repository<Teacher, EducationDbContext>, ITeacherRepository
{
    private readonly EducationDbContext _context;

    public TeacherRepository(EducationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(UserId teacherId, CancellationToken cancellationToken = default)
        => await _context.Teachers.AnyAsync(x => x.Id == teacherId, cancellationToken);

    public async Task<bool> ExistsWithinInstitutionAsync(Email email, InstitutionId institutionId, CancellationToken cancellationToken = default)
        => await _context.Teachers.AnyAsync(x => x.Email == email && x.InstitutionId == institutionId, cancellationToken);
}