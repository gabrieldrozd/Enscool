using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Domain.Institutions;

namespace Modules.Management.Application.Features.Institutions.Queries.GetDetails;

public class GetInstitutionDetailsQueryDto : IWithMapFrom<Institution, GetInstitutionDetailsQueryDto>
{
    public static GetInstitutionDetailsQueryDto From(Institution source)
    {
        throw new NotImplementedException();
    }
}