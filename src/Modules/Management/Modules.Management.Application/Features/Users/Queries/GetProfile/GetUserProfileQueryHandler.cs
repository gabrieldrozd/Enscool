using Common.Utilities.Primitives.Results;
using Core.Application.Communication.Internal.Queries;
using Core.Domain.Shared.Enumerations.UserStates;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Features.Access.Queries.GetCurrentUser;

namespace Modules.Management.Application.Features.Users.Queries.GetProfile;

internal sealed class GetUserProfileQueryHandler : IQueryHandler<GetUserProfileQuery, GetUserProfileQueryDto>
{
    private readonly IManagementDbContext _context;

    public GetUserProfileQueryHandler(IManagementDbContext context)
    {
        _context = context;
    }

    public async Task<Result<GetUserProfileQueryDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Where(x => x.Id == request.UserId)
            .AsNoTracking()
            .Select(GetUserProfileQueryDto.Mapper)
            .SingleOrDefaultAsync(cancellationToken);

        return user?.State is not UserState.Active
            ? Result.Failure.Unauthorized<GetUserProfileQueryDto>()
            : Result.Success.Ok(user);
    }
}