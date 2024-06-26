using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Restore;

internal sealed class RestoreInstitutionUserCommandValidator : AbstractValidator<RestoreInstitutionUserCommand>
{
    public RestoreInstitutionUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEqual(Guid.Empty);
    }
}