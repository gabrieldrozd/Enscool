using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.ActivateInstitutionUser;

internal sealed class ActivateInstitutionUserCommandValidator : AbstractValidator<ActivateInstitutionUserCommand>
{
    public ActivateInstitutionUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
    }
}