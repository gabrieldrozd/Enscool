using Common.Utilities.Primitives.Results;
using Core.Application.Communication.Internal.Queries.Browse;
using Core.Application.Extensions;
using Core.Application.Queries.Browse;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUsers;

/// <summary>
/// Gets the list of <see cref="InstitutionUser"/>s.
/// </summary>
public sealed record BrowseInstitutionUsersQuery : BrowseQuery<BrowseInstitutionUsersQueryDto>
{
    public Guid InstitutionId { get; init; }
    public List<UserRole> Roles { get; init; } = [];
    public List<UserState> States { get; init; } = [];

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

            if (request.States.Contains(UserState.Deleted))
                query = query.IgnoreQueryFilters();

            var results = await query
                .Where(x => x.InstitutionId! == request.InstitutionId)
                .WhereIf(request.States.Count > 0, x => request.States.Contains(x.State))
                .WhereIf(request.Roles.Count > 0, x => request.Roles.Contains(x.Role))
                .BrowseAsync(
                    request.Model,
                    [
                        x => x.FirstName,
                        x => x.MiddleName,
                        x => x.LastName,
                        x => x.Email
                    ],
                    cancellationToken)
                .MapResult<InstitutionUser, BrowseInstitutionUsersQueryDto>();

            return Result.Success.Ok(results);
        }
    }
}