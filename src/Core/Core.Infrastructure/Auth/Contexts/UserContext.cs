using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Common.Utilities.Exceptions;
using Common.Utilities.Extensions;
using Core.Application.Auth;
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

    [MemberNotNullWhen(true, nameof(Token), nameof(Expires), nameof(UserId), nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone), nameof(State), nameof(Role))]
    public bool Authenticated => ClaimsPrincipal?.Identity?.IsAuthenticated ?? false;

    public string? Token => RequestMetadataExtractor.GetToken(_httpContextAccessor?.HttpContext);

    public Date? Expires => ClaimsExtractor.GetExpires(ClaimsPrincipal?.Claims);
    public UserId? UserId => ClaimsExtractor.GetUserId(ClaimsPrincipal?.Claims);
    public InstitutionId? InstitutionId => ClaimsExtractor.GetUserInstitutionId(ClaimsPrincipal?.Claims);
    public IEnumerable<InstitutionId> InstitutionIds => ClaimsExtractor.GetUserInstitutionIds(ClaimsPrincipal?.Claims);
    public string? FirstName => ClaimsExtractor.GetUserFullName(ClaimsPrincipal?.Claims)?.FirstName;
    public string? MiddleName => ClaimsExtractor.GetUserFullName(ClaimsPrincipal?.Claims)?.MiddleName;
    public string? LastName => ClaimsExtractor.GetUserFullName(ClaimsPrincipal?.Claims)?.LastName;
    public Email? Email => ClaimsExtractor.GetUserEmail(ClaimsPrincipal?.Claims);
    public Phone? Phone => ClaimsExtractor.GetUserPhone(ClaimsPrincipal?.Claims);
    public UserState? State => ClaimsExtractor.GetUserState(ClaimsPrincipal?.Claims);
    public UserRole? Role => ClaimsExtractor.GetUserRole(ClaimsPrincipal?.Claims);

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool IsInRole(IEnumerable<UserRole> requiredRoles)
        => Role is not null && requiredRoles.HasOther(Role.Value);

    [MemberNotNull(nameof(Token), nameof(Expires), nameof(UserId), nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone), nameof(State), nameof(Role))]
    public void EnsureAuthenticated()
    {
        if (!Authenticated)
            throw new NotAuthenticatedException();
    }

    [MemberNotNull(nameof(Token), nameof(Expires), nameof(UserId), nameof(FirstName), nameof(LastName), nameof(Email), nameof(Phone), nameof(State), nameof(Role), nameof(InstitutionId))]
    public void EnsureInstitutionUserAuthenticated()
    {
        EnsureAuthenticated();
        if (InstitutionId is null)
            throw new NotAuthenticatedException();
    }
}