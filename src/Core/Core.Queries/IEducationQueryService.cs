using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;

namespace Core.Queries;

public interface IEducationQueryService
{
    /// <summary>
    /// Checks if user can be deleted.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="role">The user role.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><c>true</c> if user can be deleted; otherwise, <c>false</c>.</returns>
    Task<bool> CanDeleteUserAsync(UserId userId, UserRole role, CancellationToken cancellationToken = default);
}