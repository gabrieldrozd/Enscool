using Core.Application.Queries.Browse;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUsers;

namespace Modules.Management.Api.Endpoints.InstitutionUsers.Browse;

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
            .RequireAuthorization()
            .ProducesEnvelope<BrowseResult<BrowseInstitutionUsersQueryDto>>(StatusCodes.Status200OK)
            .WithDocumentation(
                "BrowseInstitutionUsers",
                "Get list of institution users",
                "Gets list of institution users.");
    }
}