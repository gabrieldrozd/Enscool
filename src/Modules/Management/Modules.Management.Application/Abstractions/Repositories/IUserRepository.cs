using Core.Application.Database;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetAsync(UserId userId, CancellationToken cancellationToken = default);
    Task<User?> GetAsync(Email email, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default);
}