using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Queries.GetCurrentUser;

public class UserDto : IWithMapFrom<User, UserDto>
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

    public static UserDto From(User source)
    {
        return new UserDto
        {
            UserId = source.Id,
            InstitutionId = source.InstitutionId,
            FirstName = source.FullName.First,
            MiddleName = source.FullName.Middle,
            LastName = source.FullName.Last,
            Email = source.Email,
            Phone = source.Phone,
            State = source.State,
            Role = source.Role
        };
    }
}