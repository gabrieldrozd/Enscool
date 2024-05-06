using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;

namespace Core.Application.Auth;

public sealed class AccessModel
{
    public string AccessToken { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
    public string RefreshToken { get; init; } = string.Empty;

    public Guid UserId { get; init; }
    public UserRole Role { get; init; }
}