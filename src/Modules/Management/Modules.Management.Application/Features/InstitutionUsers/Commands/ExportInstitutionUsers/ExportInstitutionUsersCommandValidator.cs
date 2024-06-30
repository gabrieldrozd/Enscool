using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.ExportInstitutionUsers;

internal sealed class ExportInstitutionUsersCommandValidator : AbstractValidator<ExportInstitutionUsersCommand>
{
    public ExportInstitutionUsersCommandValidator()
    {
        RuleFor(x => x.InstitutionId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.UserIds).ForEach(x => x.NotEmpty().NotEqual(Guid.Empty));
    }
}