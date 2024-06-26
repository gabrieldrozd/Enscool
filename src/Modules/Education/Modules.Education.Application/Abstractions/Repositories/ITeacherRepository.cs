using Core.Application.Database;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Modules.Education.Domain.Teachers;

namespace Modules.Education.Application.Abstractions.Repositories;

public interface ITeacherRepository : IRepository<Teacher>
{
    Task<bool> ExistsAsync(UserId teacherId, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithinInstitutionAsync(Email email, InstitutionId institutionId, CancellationToken cancellationToken = default);
    Task<Teacher?> GetAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<Teacher?> GetDeletedAsync(UserId userId, CancellationToken cancellationToken = default);
}