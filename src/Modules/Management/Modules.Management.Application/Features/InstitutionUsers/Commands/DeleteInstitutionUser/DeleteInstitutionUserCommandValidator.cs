using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.DeleteInstitutionUser;

internal sealed class DeleteInstitutionUserCommandValidator : AbstractValidator<DeleteInstitutionUserCommand>
{
    public DeleteInstitutionUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEqual(Guid.Empty);
    }
}