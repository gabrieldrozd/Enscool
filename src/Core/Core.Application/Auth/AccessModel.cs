using Core.Domain.Shared.Enumerations.Roles;

namespace Core.Application.Auth;

public sealed class AccessModel
{
    public string AccessToken { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
    public string RefreshToken { get; init; } = string.Empty;

    public Guid UserId { get; init; }
    public Guid? InstitutionId { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public UserRole Role { get; init; }
}