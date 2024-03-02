using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;

namespace Core.Application.Abstractions.Auth;

public sealed class AccessToken
{
    public string Token { get; init; } = string.Empty;
    public long Expires { get; init; }
    public Guid UserId { get; init; }
    public Guid? InstitutionId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Phone { get; init; }
    public UserState State { get; init; }
    public UserRole Role { get; init; }
}