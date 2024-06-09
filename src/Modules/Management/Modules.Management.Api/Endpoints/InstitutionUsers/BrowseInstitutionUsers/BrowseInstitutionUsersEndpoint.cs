using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUserProfile;

namespace Modules.Management.Api.Endpoints.InstitutionUsers.BrowseInstitutionUsers;

/// <summary>
/// Get current user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class BrowseInstitutionUsersEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                "browse",
                ManagementEndpointInfo.InstitutionUsers,
                async ([FromBody] BrowseInstitutionUsersRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .RequireAuthorization()
            .ProducesEnvelope<GetInstitutionUserProfileQueryDto>(StatusCodes.Status200OK)
            .WithDocumentation(
                "BrowseInstitutionUsers",
                "Get list of institution users",
                "Gets list of institution users.");
    }
}