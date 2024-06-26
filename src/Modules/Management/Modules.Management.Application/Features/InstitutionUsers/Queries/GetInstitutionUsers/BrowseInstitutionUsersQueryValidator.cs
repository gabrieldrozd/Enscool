using FluentValidation;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUsers;

internal sealed class BrowseInstitutionUsersQueryValidator : AbstractValidator<BrowseInstitutionUsersQuery>
{
    public BrowseInstitutionUsersQueryValidator()
    {
        RuleFor(x => x.InstitutionId).NotEmpty().NotEqual(Guid.Empty);
    }
}