using Core.Domain.Shared.Enumerations.UserStates;
using Modules.Management.Domain.Users;
using Wisse.Core.Application.Abstractions.Communication.Internal.Queries.Browse;

namespace Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUsers;

/// <summary>
/// Gets the list of <see cref="InstitutionUser"/>s.
/// </summary>
public sealed record BrowseInstitutionUsersQuery(Guid InstitutionId, UserState? State = null) : BrowseQuery<BrowseInstitutionUsersQueryDto>;