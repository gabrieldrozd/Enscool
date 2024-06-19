using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Queries.GetInstitutionUserProfile;

namespace Modules.Management.Api.Endpoints.InstitutionUsers.GetInstitutionUserProfile;

/// <summary>
/// Get current user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class GetInstitutionUserProfileEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                "profile",
                ManagementEndpointInfo.InstitutionUsers,
                async (ISender sender) =>
                {
                    var result = await sender.Send(new GetInstitutionUserProfileQuery());
                    return BuildEnvelope(result);
                })
            .RequireInstitutionRoles()
            .ProducesEnvelope<GetInstitutionUserProfileQueryDto>(StatusCodes.Status200OK)
            .WithDocumentation(
                "GetInstitutionUserProfile",
                "Get current institution user profile",
                "Gets profile of currently logged in institution user.");
    }
}