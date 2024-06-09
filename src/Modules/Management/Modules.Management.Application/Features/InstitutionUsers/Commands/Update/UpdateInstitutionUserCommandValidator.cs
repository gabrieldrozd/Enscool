using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Update;

internal sealed class UpdateInstitutionUserCommandValidator : AbstractValidator<UpdateInstitutionUserCommand>
{
    public UpdateInstitutionUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEqual(Guid.Empty);
        RuleFor(x => x.Phone).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }
}