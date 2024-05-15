using Common.Utilities.Primitives.Results;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Queries;
using Core.Domain.Shared.Enumerations.UserStates;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;

namespace Modules.Management.Application.Features.Access.Queries.GetCurrentUser;

internal sealed class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, GetCurrentUserQueryDto>
{
    private readonly IUserContext _userContext;
    private readonly IManagementDbContext _context;

    public GetCurrentUserQueryHandler(IUserContext userContext, IManagementDbContext context)
    {
        _userContext = userContext;
        _context = context;
    }

    public async Task<Result<GetCurrentUserQueryDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        _userContext.EnsureInstitutionUserAuthenticated();

        var user = await _context.Users
            .Where(x => x.Id == _userContext.UserId)
            .AsNoTracking()
            .Select(GetCurrentUserQueryDto.GetMapping())
            .SingleOrDefaultAsync(cancellationToken);

        return user?.State is not UserState.Active
            ? Result.Failure.Unauthorized<GetCurrentUserQueryDto>()
            : Result.Success.Ok(user);
    }
}