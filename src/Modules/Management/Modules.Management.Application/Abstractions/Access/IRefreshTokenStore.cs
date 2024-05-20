using Core.Application.Auth;
using Core.Domain.Shared.EntityIds;

namespace Modules.Management.Application.Abstractions.Access;

public interface IRefreshTokenStore
{
    RefreshToken Generate(UserId userId);
    Task StoreAsync(RefreshToken refreshToken);
    Task<bool> ValidateAsync(Guid userId, string token);
    Task RevokeAsync(Guid userId);
}