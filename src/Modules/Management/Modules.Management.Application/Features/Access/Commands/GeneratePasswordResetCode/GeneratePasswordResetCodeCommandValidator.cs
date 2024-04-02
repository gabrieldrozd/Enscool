using FluentValidation;

namespace Modules.Management.Application.Features.Access.Commands.GeneratePasswordResetCode;

internal sealed class GeneratePasswordResetCodeCommandValidator : AbstractValidator<GeneratePasswordResetCodeCommand>
{
    public GeneratePasswordResetCodeCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}