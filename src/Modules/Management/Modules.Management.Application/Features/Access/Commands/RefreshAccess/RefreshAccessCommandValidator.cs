using FluentValidation;

namespace Modules.Management.Application.Features.Access.Commands.RefreshAccess;

internal sealed class RefreshAccessCommandValidator : AbstractValidator<RefreshAccessCommand>
{
    public RefreshAccessCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}