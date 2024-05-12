using Core.Application.Communication.Internal.Queries;

namespace Modules.Management.Application.Features.Users.Queries.GetProfile;

public sealed record GetUserProfileQuery(Guid UserId) : IQuery<GetUserProfileQueryDto>;