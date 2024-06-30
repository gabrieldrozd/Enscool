using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUserDetails;

namespace Modules.Management.Api.Endpoints.InstitutionUsers;

/// <summary>
/// Get current user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class GetInstitutionUserDetailsEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                "{userId:guid}",
                ManagementEndpointInfo.InstitutionUsers,
                async (Guid userId, ISender sender) =>
                {
                    var result = await sender.Send(new GetInstitutionUserDetailsQuery(userId));
                    return BuildEnvelope(result);
                })
            .RequireInstitutionRoles()
            .ProducesEnvelope<GetInstitutionUserDetailsQueryDto>(StatusCodes.Status200OK)
            .WithDocumentation(
                "GetInstitutionUserDetails",
                "Get institution user details",
                "Gets details of institution user by user id.");
    }
}