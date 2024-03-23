using System.Security.Claims;
using Core.Application.Auth;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;

namespace Core.Infrastructure.Auth.Contexts;

internal static class ClaimsExtractor
{
    internal static Date? GetExpires(IEnumerable<Claim>? claims)
    {
        return claims?
            .Where(x => x.Type == ClaimConsts.Expires)
            .Select(x => !string.IsNullOrWhiteSpace(x.Value) ? Date.FromUnixSeconds(long.Parse(x.Value)) : null)
            .FirstOrDefault();
    }

    internal static UserId? GetUserId(IEnumerable<Claim>? claims)
    {
        return claims?
            .Where(x => x.Type == ClaimConsts.UserId)
            .Select(x => Guid.TryParse(x.Value, out var id) ? UserId.From(id) : null)
            .FirstOrDefault();
    }

    internal static InstitutionId? GetUserInstitutionId(IEnumerable<Claim>? claims)
    {
        return claims?
            .Where(x => x.Type == ClaimConsts.InstitutionId)
            .Select(x => Guid.TryParse(x.Value, out var id) ? InstitutionId.From(id) : null)
            .FirstOrDefault();
    }

    internal static List<InstitutionId> GetUserInstitutionIds(IEnumerable<Claim>? claims)
    {
        return claims?
            .FirstOrDefault(x => x.Type == ClaimConsts.InstitutionIds)!.Value
            .Split(';', StringSplitOptions.RemoveEmptyEntries)
            .Select(InstitutionId.From)
            .ToList() ?? [];
    }

    internal static FullName? GetUserFullName(IEnumerable<Claim>? claims)
    {
        return claims?
            .Where(x => x.Type == ClaimConsts.FullName)
            .Select(x => !string.IsNullOrWhiteSpace(x.Value) ? FullName.FromString(x.Value) : null)
            .FirstOrDefault();
    }

    internal static Email? GetUserEmail(IEnumerable<Claim>? claims)
    {
        return claims?
            .Where(x => x.Type == ClaimConsts.Email)
            .Select(x => !string.IsNullOrWhiteSpace(x.Value) ? Email.Parse(x.Value) : null)
            .FirstOrDefault();
    }

    internal static Phone? GetUserPhone(IEnumerable<Claim>? claims)
    {
        return claims?
            .Where(x => x.Type == ClaimConsts.Phone)
            .Select(x => !string.IsNullOrWhiteSpace(x.Value) ? Phone.Parse(x.Value) : null)
            .FirstOrDefault();
    }

    internal static UserState? GetUserState(IEnumerable<Claim>? claims)
    {
        return claims?
            .Where(x => x.Type == ClaimConsts.UserState)
            .Select(x => !string.IsNullOrWhiteSpace(x.Value) ? (UserState?) int.Parse(x.Value) : null)
            .FirstOrDefault();
    }

    internal static UserRole? GetUserRole(IEnumerable<Claim>? claims)
    {
        return claims?
            .Where(x => x.Type == ClaimConsts.UserRole)
            .Select(x => !string.IsNullOrWhiteSpace(x.Value) ? (UserRole?) int.Parse(x.Value) : null)
            .FirstOrDefault();
    }
}