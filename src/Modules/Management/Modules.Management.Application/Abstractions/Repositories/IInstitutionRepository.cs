using Core.Application.Database;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Domain.Institutions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions.Repositories;

public interface IInstitutionRepository : IRepository<Institution>
{
    Task<bool> ExistsAsync(InstitutionId institutionId, CancellationToken cancellationToken = default);
    Task<Institution?> GetAsync(InstitutionId institutionId, CancellationToken cancellationToken = default);
}