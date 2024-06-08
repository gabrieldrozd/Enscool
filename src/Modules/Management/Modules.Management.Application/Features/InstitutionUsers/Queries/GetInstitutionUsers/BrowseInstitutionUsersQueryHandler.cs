using Common.Utilities.Extensions;
using Common.Utilities.Primitives.Results;
using Core.Application.Queries.Browse;
using Core.Domain.Shared.Enumerations.UserStates;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Domain.Users;
using Wisse.Core.Application.Abstractions.Communication.Internal.Queries.Browse;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUsers;

internal sealed class BrowseInstitutionUsersQueryHandler : BrowseQueryHandler<BrowseInstitutionUsersQuery, BrowseInstitutionUsersQueryDto>
{
    private readonly IManagementDbContext _context;

    public BrowseInstitutionUsersQueryHandler(IManagementDbContext context)
    {
        _context = context;
    }

    public override async Task<Result<BrowseResult<BrowseInstitutionUsersQueryDto>>> Handle(BrowseInstitutionUsersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Users.OfType<InstitutionUser>().AsQueryable();

        if (request.State is UserState.Deleted)
            query = query.IgnoreQueryFilters();

        var results = await query
            .WhereIf(request.InstitutionId != Guid.Empty, x => x.InstitutionId! == request.InstitutionId)
            .WhereIf(request.State is not null, x => x.State == request.State)
            .BrowseAsync(
                request.Model,
                [
                    x => x.FullName,
                    x => x.Email
                ],
                cancellationToken)
            .MapResult<InstitutionUser, BrowseInstitutionUsersQueryDto>();

        return Result.Success.Ok(results);
    }
}