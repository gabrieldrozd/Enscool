using System.Linq.Expressions;
using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Queries.GetProfile;

public class GetUserProfileQueryDto : IWithExpressionMapFrom<User, GetUserProfileQueryDto>
{
    public Guid UserId { get; private init; }
    public Guid? InstitutionId { get; private init; }
    public string FirstName { get; private init; } = string.Empty;
    public string? MiddleName { get; private init; }
    public string LastName { get; private init; } = string.Empty;
    public string Email { get; private init; } = string.Empty;
    public string? Phone { get; private init; }
    public UserState State { get; private init; }
    public UserRole Role { get; private init; }

    public static Expression<Func<User, GetUserProfileQueryDto>> Mapper =>
        user => new GetUserProfileQueryDto
        {
            UserId = user.Id,
            InstitutionId = user.InstitutionId,
            FirstName = user.FullName.First,
            MiddleName = user.FullName.Middle,
            LastName = user.FullName.Last,
            Email = user.Email,
            Phone = user.Phone,
            State = user.State,
            Role = user.Role
        };
}