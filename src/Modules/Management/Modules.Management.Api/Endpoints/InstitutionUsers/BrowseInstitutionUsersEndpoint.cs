using Common.Utilities.Abstractions.Mapping;
using Core.Application.Queries.Browse;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUsers;

namespace Modules.Management.Api.Endpoints.InstitutionUsers;

/// <summary>
/// Get current user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class BrowseInstitutionUsersEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "browse",
                ManagementEndpointInfo.InstitutionUsers,
                async ([FromBody] BrowseInstitutionUsersRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .RequireRoles(UserRole.InstitutionAdmin, UserRole.Secretary, UserRole.Teacher)
            .ProducesEnvelope<BrowseResult<BrowseInstitutionUsersQueryDto>>(StatusCodes.Status200OK)
            .WithDocumentation(
                "BrowseInstitutionUsers",
                "Get list of institution users",
                "Gets list of institution users.");
    }
}

internal sealed class BrowseInstitutionUsersRequest : IWithMapTo<BrowseInstitutionUsersQuery>
{
    public BrowseModel? Model { get; init; }
    public Guid InstitutionId { get; set; }
    public List<UserRole> Roles { get; set; } = [];
    public List<UserState> States { get; set; } = [];

    public BrowseInstitutionUsersQuery Map() => new()
    {
        Model = Model,
        InstitutionId = InstitutionId,
        Roles = Roles,
        States = States
    };
}