using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Queries.Base;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUserDetails;

/// <summary>
/// Gets the list of <see cref="InstitutionUser"/>s.
/// </summary>
public sealed record GetInstitutionUserDetailsQuery : IQuery<GetInstitutionUserDetailsQueryDto>
{
    public Guid UserId { get; init; }

    public GetInstitutionUserDetailsQuery(Guid userId)
    {
        UserId = userId;
    }

    internal sealed class Handler : IQueryHandler<GetInstitutionUserDetailsQuery, GetInstitutionUserDetailsQueryDto>
    {
        private readonly IManagementDbContext _context;

        public Handler(IManagementDbContext context) => _context = context;

        public async Task<Result<GetInstitutionUserDetailsQueryDto>> Handle(GetInstitutionUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .OfType<InstitutionUser>()
                .AsNoTracking()
                .Select(GetInstitutionUserDetailsQueryDto.GetMapping())
                .SingleOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

            return user.OrNotFound(Resource.UserNotFound, request.UserId);
        }
    }
}