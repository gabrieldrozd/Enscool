using System.Diagnostics.CodeAnalysis;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Core.Application.Auth;

public interface IUserContext
{
    [MemberNotNullWhen(true, nameof(Token), nameof(Expires), nameof(UserId), nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone), nameof(State), nameof(Role))]
    bool Authenticated { get; }

    string? Token { get; }

    public Date? Expires { get; }
    public UserId? UserId { get; }
    public InstitutionId? InstitutionId { get; }
    public IEnumerable<InstitutionId> InstitutionIds { get; }
    public string? FirstName { get; }
    public string? MiddleName { get; }
    public string? LastName { get; }
    public Email? Email { get; }
    public Phone? Phone { get; }
    public UserState? State { get; }
    public UserRole? Role { get; }

    bool IsInRole(IEnumerable<UserRole> requiredRoles);

    [MemberNotNull(nameof(Token), nameof(Expires), nameof(UserId), nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone), nameof(State), nameof(Role))]
    void EnsureAuthenticated();

    [MemberNotNull(nameof(Token), nameof(Expires), nameof(UserId), nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone), nameof(State), nameof(Role), nameof(InstitutionId))]
    void EnsureInstitutionUserAuthenticated();
}