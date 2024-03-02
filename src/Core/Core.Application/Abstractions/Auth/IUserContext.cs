using System.Diagnostics.CodeAnalysis;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Core.Application.Abstractions.Auth;

public interface IUserContext
{
    [MemberNotNullWhen(true, nameof(Expires), nameof(UserId), nameof(Fullname), nameof(Email), nameof(Phone), nameof(State), nameof(Role))]
    bool Authenticated { get; }

    public Date? Expires { get; }
    public UserId? UserId { get; }
    public InstitutionId? InstitutionId { get; }
    public IEnumerable<InstitutionId> InstitutionIds { get; }
    public Fullname? Fullname { get; }
    public Email? Email { get; }
    public Phone? Phone { get; }
    public UserState? State { get; }
    public UserRole? Role { get; }

    bool IsInRole(IEnumerable<UserRole> requiredRoles);

    [MemberNotNull(nameof(Expires), nameof(UserId), nameof(Fullname), nameof(Email), nameof(Phone), nameof(State), nameof(Role))]
    void EnsureAuthenticated();

    [MemberNotNull(nameof(Expires), nameof(UserId), nameof(Fullname), nameof(Email), nameof(Phone), nameof(State), nameof(Role), nameof(InstitutionId))]
    void EnsureInstitutionUserAuthenticated();
}