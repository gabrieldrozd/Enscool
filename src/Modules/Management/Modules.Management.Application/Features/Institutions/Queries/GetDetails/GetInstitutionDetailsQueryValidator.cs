using FluentValidation;

namespace Modules.Management.Application.Features.Institutions.Queries.GetDetails;

internal sealed class GetInstitutionDetailsQueryValidator : AbstractValidator<GetInstitutionDetailsQuery>
{
    public GetInstitutionDetailsQueryValidator()
    {
        RuleFor(x => x.InstitutionId).NotEmpty().NotEqual(Guid.Empty);
    }
}