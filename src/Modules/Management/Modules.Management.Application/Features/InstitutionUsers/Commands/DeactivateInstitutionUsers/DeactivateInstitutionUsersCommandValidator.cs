using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.DeactivateInstitutionUsers;

internal sealed class DeactivateInstitutionUsersCommandValidator : AbstractValidator<DeactivateInstitutionUsersCommand>
{
    public DeactivateInstitutionUsersCommandValidator()
    {
        RuleFor(x => x.UserIds).ForEach(x => x.NotEmpty().NotEqual(Guid.Empty));
    }
}