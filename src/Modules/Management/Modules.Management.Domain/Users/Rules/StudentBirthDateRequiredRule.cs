using Common.Utilities.Resources;
using Core.Domain.Primitives.Rules;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.ValueObjects;

namespace Modules.Management.Domain.Users.Rules;

public class StudentBirthDateRequiredRule : IBusinessRule
{
    private readonly InstitutionUserRole _role;
    private readonly Date? _birthDate;

    public StudentBirthDateRequiredRule(InstitutionUserRole role, Date? birthDate)
    {
        _role = role;
        _birthDate = birthDate;
    }

    public string Message => Resource.StudentBirthDateRequired;
    public bool IsInvalid() => _role is InstitutionUserRole.Student && _birthDate is null;
}