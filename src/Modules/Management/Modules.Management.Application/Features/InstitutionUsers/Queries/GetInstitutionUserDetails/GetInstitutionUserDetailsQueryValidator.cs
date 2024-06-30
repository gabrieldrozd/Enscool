using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUserDetails;

internal sealed class GetInstitutionUserDetailsQueryValidator : AbstractValidator<GetInstitutionUserDetailsQuery>
{
    public GetInstitutionUserDetailsQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().NotEqual(Guid.Empty);
    }
}