using Common.Utilities.Primitives.Results;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Queries.Base;
using Core.Domain.Shared.Enumerations.UserStates;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUserProfile;

/// <summary>
/// Gets the <see cref="InstitutionUser"/> profile.
/// </summary>
public sealed record GetInstitutionUserProfileQuery : IQuery<GetInstitutionUserProfileQueryDto>
{
    internal sealed class Handler : IQueryHandler<GetInstitutionUserProfileQuery, GetInstitutionUserProfileQueryDto>
    {
        private readonly IUserContext _userContext;
        private readonly IManagementDbContext _context;

        public Handler(IUserContext userContext, IManagementDbContext context)
        {
            _userContext = userContext;
            _context = context;
        }

        public async Task<Result<GetInstitutionUserProfileQueryDto>> Handle(GetInstitutionUserProfileQuery request, CancellationToken cancellationToken)
        {
            _userContext.EnsureInstitutionUserAuthenticated();

            var user = await _context.Users
                .OfType<InstitutionUser>()
                .AsNoTracking()
                .Select(GetInstitutionUserProfileQueryDto.GetMapping())
                .SingleOrDefaultAsync(x => x.UserId == _userContext.UserId, cancellationToken);

            return user?.State is not UserState.Active
                ? Result.Failure.Unauthorized<GetInstitutionUserProfileQueryDto>()
                : Result.Success.Ok(user);
        }
    }
}