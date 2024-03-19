using Core.Application.Database;
using Core.Domain.Shared.EntityIds;
using Modules.Management.Domain.Institutions;

namespace Modules.Management.Application.Abstractions.Repositories;

public interface IInstitutionRepository : IRepository<Institution>
{
    Task<bool> ExistsAsync(InstitutionId institutionId, CancellationToken cancellationToken = default);
    Task<Institution?> GetAsync(InstitutionId institutionId, CancellationToken cancellationToken = default);
}