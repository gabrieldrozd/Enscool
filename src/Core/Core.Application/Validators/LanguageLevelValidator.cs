using Core.Domain.Shared.Enumerations.Languages;
using FluentValidation;

namespace Core.Application.Validators;

public class LanguageLevelValidator : AbstractValidator<int>
{
    public LanguageLevelValidator()
    {
        RuleFor(x => x).Custom((value, context) =>
        {
            if (!LanguageLevel.IsValid(value)) context.AddFailure($"Invalid language level value");
        });
    }
}