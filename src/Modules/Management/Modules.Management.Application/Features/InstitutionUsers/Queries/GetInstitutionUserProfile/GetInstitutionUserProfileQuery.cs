using Core.Application.Communication.Internal.Queries;
using Core.Application.Communication.Internal.Queries.Base;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUserProfile;

/// <summary>
/// Gets the <see cref="InstitutionUser"/> profile.
/// </summary>
public sealed record GetInstitutionUserProfileQuery : IQuery<GetInstitutionUserProfileQueryDto>;