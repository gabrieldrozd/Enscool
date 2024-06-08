using Core.Application.Communication.Internal.Queries;
using Core.Application.Communication.Internal.Queries.Base;
using Modules.Management.Domain.Institutions;

namespace Modules.Management.Application.Features.Institutions.Queries.GetDetails;

/// <summary>
/// Gets <see cref="Institution"/> details.
/// </summary>
/// <param name="InstitutionId"></param>
public sealed record GetInstitutionDetailsQuery(Guid InstitutionId) : IQuery<GetInstitutionDetailsQueryDto>;