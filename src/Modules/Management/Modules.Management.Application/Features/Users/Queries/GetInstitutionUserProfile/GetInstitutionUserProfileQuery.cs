using Core.Application.Communication.Internal.Queries;

namespace Modules.Management.Application.Features.Users.Queries.GetInstitutionUserProfile;

public sealed record GetInstitutionUserProfileQuery(Guid UserId) : IQuery<GetInstitutionUserProfileQueryDto>;