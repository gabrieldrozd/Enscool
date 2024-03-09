using Core.Application.Database;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsWithEmailAsync(Email email, CancellationToken cancellationToken = default);
}