using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Delete;

internal sealed class DeleteInstitutionUserCommandValidator : AbstractValidator<DeleteInstitutionUserCommand>
{
    public DeleteInstitutionUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEqual(Guid.Empty);
    }
}