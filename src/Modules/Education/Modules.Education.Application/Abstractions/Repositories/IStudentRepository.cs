using Core.Application.Database;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Abstractions.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    Task<bool> ExistsAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithinInstitutionAsync(Email email, InstitutionId institutionId, CancellationToken cancellationToken = default);
    Task<Student?> GetAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<Student?> GetAsync(Email email, CancellationToken cancellationToken = default);
}