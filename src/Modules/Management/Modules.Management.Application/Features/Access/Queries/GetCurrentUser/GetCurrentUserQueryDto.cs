using System.Linq.Expressions;
using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Queries.GetCurrentUser;

public class GetCurrentUserQueryDto : IWithExpressionMapFrom<User, GetCurrentUserQueryDto>
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

    public static Expression<Func<User, GetCurrentUserQueryDto>> Mapper =>
        user => new GetCurrentUserQueryDto
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