using Common.Utilities.Resources;
using Core.Domain.Primitives.Rules;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.Roles;

namespace Modules.Management.Domain.Users.Rules;

public class StudentLanguageLevelRequiredRule : IBusinessRule
{
    private readonly InstitutionUserRole _role;
    private readonly LanguageLevel? _languageLevel;

    public StudentLanguageLevelRequiredRule(InstitutionUserRole role, LanguageLevel? languageLevel)
    {
        _role = role;
        _languageLevel = languageLevel;
    }

    public string Message => Resource.StudentLanguageLevelRequired;
    public bool IsInvalid() => _role is InstitutionUserRole.Student && _languageLevel is null;
}