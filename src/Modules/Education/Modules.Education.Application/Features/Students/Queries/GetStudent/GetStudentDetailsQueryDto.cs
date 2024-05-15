using System.Linq.Expressions;
using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.DTOs;
using Core.Domain.Shared.Enumerations.UserStates;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Features.Students.Queries.GetStudent;

public class GetStudentDetailsQueryDto : IWithExpressionMapFrom<Student, GetStudentDetailsQueryDto>
{
    public Guid UserId { get; private init; }
    public Guid? InstitutionId { get; private init; }
    public string FirstName { get; private init; } = string.Empty;
    public string? MiddleName { get; private init; }
    public string LastName { get; private init; } = string.Empty;
    public string Email { get; private init; } = string.Empty;
    public string? Phone { get; private init; }
    public UserState State { get; private init; }
    public AddressDto Address { get; set; }
    public string LanguageLevel { get; private set; } = null!;
    public DateTime BirthDate { get; private set; }

    public static Expression<Func<Student, GetStudentDetailsQueryDto>> GetMapping() =>
        student => new GetStudentDetailsQueryDto
        {
            UserId = student.Id,
            InstitutionId = student.InstitutionId,
            FirstName = student.FullName.First,
            MiddleName = student.FullName.Middle,
            LastName = student.FullName.Last,
            Email = student.Email.Value,
            Phone = student.Phone.Value,
            State = student.State,
            Address = AddressDto.From(student.Address),
            LanguageLevel = student.LanguageLevel.ToString(),
            BirthDate = student.BirthDate.DateTime!.Value
        };
}