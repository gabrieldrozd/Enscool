using FluentValidation;

namespace Modules.Management.Application.Features.Access.Commands.ActivateAccount;

internal sealed class ActivateAccountCommandValidator : AbstractValidator<ActivateAccountCommand>
{
    public ActivateAccountCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.Code).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}