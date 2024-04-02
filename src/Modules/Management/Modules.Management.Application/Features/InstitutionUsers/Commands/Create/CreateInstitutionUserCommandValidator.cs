using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Create;

internal sealed class CreateInstitutionUserCommandValidator : AbstractValidator<CreateInstitutionUserCommand>
{
    public CreateInstitutionUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.InstitutionId).NotEmpty().NotEqual(Guid.Empty);
    }
}