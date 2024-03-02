using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Common.Utilities.Exceptions;
using Common.Utilities.Extensions;
using Core.Application.Abstractions.Auth;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure.Auth.Contexts;

internal sealed class UserContext : IUserContext
{
    private readonly IHttpContextAccessor? _httpContextAccessor;

    private ClaimsPrincipal? ClaimsPrincipal => _httpContextAccessor?.HttpContext?.User;

    [MemberNotNullWhen(true, nameof(Expires), nameof(UserId), nameof(Fullname), nameof(Email), nameof(Phone), nameof(State), nameof(Role))]
    public bool Authenticated => ClaimsPrincipal?.Identity?.IsAuthenticated ?? false;

    public Date? Expires => ClaimsExtractor.GetExpires(ClaimsPrincipal);
    public UserId? UserId => ClaimsExtractor.GetUserId(ClaimsPrincipal);
    public InstitutionId? InstitutionId => ClaimsExtractor.GetUserInstitutionId(ClaimsPrincipal);
    public IEnumerable<InstitutionId> InstitutionIds => ClaimsExtractor.GetUserInstitutionIds(ClaimsPrincipal);
    public Fullname? Fullname => ClaimsExtractor.GetUserFullname(ClaimsPrincipal);
    public Email? Email => ClaimsExtractor.GetUserEmail(ClaimsPrincipal);
    public Phone? Phone => ClaimsExtractor.GetUserPhone(ClaimsPrincipal);
    public UserState? State => ClaimsExtractor.GetUserState(ClaimsPrincipal);
    public UserRole? Role => ClaimsExtractor.GetUserRole(ClaimsPrincipal);

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsInRole(IEnumerable<UserRole> requiredRoles)
        => Role is not null && requiredRoles.HasOther(Role.Value);

    [MemberNotNull(nameof(Expires), nameof(UserId), nameof(Fullname), nameof(Email), nameof(Phone), nameof(State), nameof(Role))]
    public void EnsureAuthenticated()
    {
        if (!Authenticated)
            throw new NotAuthenticatedException();
    }

    [MemberNotNull(nameof(Expires), nameof(UserId), nameof(Fullname), nameof(Email), nameof(Phone), nameof(State), nameof(Role), nameof(InstitutionId))]
    public void EnsureInstitutionUserAuthenticated()
    {
        EnsureAuthenticated();
        if (InstitutionId is null)
            throw new NotAuthenticatedException();
    }
}