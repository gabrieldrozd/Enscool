using System.Linq.Expressions;
using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.DTOs;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUserProfile;

public class GetInstitutionUserProfileQueryDto : IWithExpressionMapFrom<InstitutionUser, GetInstitutionUserProfileQueryDto>
{
    public Guid UserId { get; private init; }
    public Guid? InstitutionId { get; private init; }
    public string FirstName { get; private init; } = null!;
    public string? MiddleName { get; private init; }
    public string LastName { get; private init; } = null!;
    public string Email { get; private init; } = null!;
    public string? Phone { get; private init; }
    public UserState State { get; private init; }
    public UserRole Role { get; private init; }
    public DateTime? BirthDate { get; private init; }
    public AddressDto? Address { get; private init; }

    public static Expression<Func<InstitutionUser, GetInstitutionUserProfileQueryDto>> GetMapping() =>
        user => new GetInstitutionUserProfileQueryDto
        {
            UserId = user.Id,
            InstitutionId = user.InstitutionId,
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            State = user.State,
            Role = user.Role,
            BirthDate = user.BirthDate,
            Address = AddressDto.FromNullable(user.Address)
        };
}