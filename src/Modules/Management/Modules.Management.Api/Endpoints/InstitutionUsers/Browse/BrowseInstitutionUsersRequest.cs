using Common.Utilities.Abstractions.Mapping;
using Core.Application.Queries.Browse;
using Core.Domain.Shared.Enumerations.UserStates;
using Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUsers;

namespace Modules.Management.Api.Endpoints.InstitutionUsers.Browse;

internal sealed class BrowseInstitutionUsersRequest : IWithMapTo<BrowseInstitutionUsersQuery>
{
    public BrowseModel? Model { get; init; }
    public Guid InstitutionId { get; set; }
    public UserState? State { get; set; }

    public BrowseInstitutionUsersQuery Map() => new()
    {
        Model = Model,
        InstitutionId = InstitutionId,
        State = State
    };
}