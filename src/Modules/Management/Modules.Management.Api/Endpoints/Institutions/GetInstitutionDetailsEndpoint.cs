using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.Institutions.Queries.GetDetails;

namespace Modules.Management.Api.Endpoints.Institutions;

/// <summary>
/// Get current user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class GetInstitutionDetailsEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                "{institutionId:guid}",
                ManagementEndpointInfo.Institutions,
                async (Guid institutionId, ISender sender) =>
                {
                    var result = await sender.Send(new GetInstitutionDetailsQuery(institutionId));
                    return BuildEnvelope(result);
                })
            .RequireAuthorization()
            .RequireInstitutionRoles()
            .ProducesEnvelope<GetInstitutionDetailsQueryDto>(StatusCodes.Status200OK)
            .WithDocumentation(
                "GetInstitutionDetails",
                "Get institution details",
                "Gets details of the institution by its id.");
    }
}