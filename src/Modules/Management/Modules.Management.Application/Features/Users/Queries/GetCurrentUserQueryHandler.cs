using Common.Utilities.Primitives.Results;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Queries;
using Core.Domain.Shared.Enumerations.UserStates;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Access;

namespace Modules.Management.Application.Features.Users.Queries;

internal sealed class GetCurrentUserQueryHandler : IQueryHandler<GetCurrentUserQuery, AccessModel>
{
    private readonly IUserContext _userContext;
    private readonly ITokenProvider _tokenProvider;
    private readonly IManagementDbContext _context;

    public GetCurrentUserQueryHandler(IUserContext userContext, ITokenProvider tokenProvider, IManagementDbContext context)
    {
        _userContext = userContext;
        _tokenProvider = tokenProvider;
        _context = context;
    }

    public async Task<Result<AccessModel>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        _userContext.EnsureInstitutionUserAuthenticated();
        var user = await _context.Users
            .Where(x => x.Id == _userContext.UserId)
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (user?.State is not UserState.Active)
            return Result.Failure.Unauthorized<AccessModel>();

        var accessToken = _tokenProvider.Create(user);
        return Result.Success.Ok(accessToken);
    }
}