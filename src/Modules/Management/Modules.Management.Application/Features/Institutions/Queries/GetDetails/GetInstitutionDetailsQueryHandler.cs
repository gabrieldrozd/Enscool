using Common.Utilities.Primitives.Results;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Queries;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;

namespace Modules.Management.Application.Features.Institutions.Queries.GetDetails;

internal sealed class GetInstitutionDetailsQueryHandler : IQueryHandler<GetInstitutionDetailsQuery, GetInstitutionDetailsQueryDto>
{
    private readonly IManagementDbContext _context;

    public GetInstitutionDetailsQueryHandler(IManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetInstitutionDetailsQueryDto>> Handle(GetInstitutionDetailsQuery request, CancellationToken cancellationToken)
    {
        var institution = await _context.Institutions
            .Where(x => x.Id == request.InstitutionId)
            .AsNoTracking()
            .Select(GetInstitutionDetailsQueryDto.GetMapping())
            .SingleOrDefaultAsync(cancellationToken);

        return institution ?? Result.Failure.NotFound<GetInstitutionDetailsQueryDto>(Resource.InstitutionNotFound, request.InstitutionId);
    }
}