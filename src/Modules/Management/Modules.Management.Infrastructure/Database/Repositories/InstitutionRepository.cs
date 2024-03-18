using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Core.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Institutions;

namespace Modules.Management.Infrastructure.Database.Repositories;

public sealed class InstitutionRepository : Repository<Institution, ManagementDbContext>, IInstitutionRepository
{
    private readonly ManagementDbContext _context;

    public InstitutionRepository(ManagementDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(InstitutionId institutionId, CancellationToken cancellationToken = default)
        => await _context.Institutions.AnyAsync(x => x.Id == institutionId, cancellationToken);

    public async Task<Institution?> GetAsync(InstitutionId institutionId, CancellationToken cancellationToken = default)
        => await _context.Institutions.FindAsync([institutionId], cancellationToken);
}