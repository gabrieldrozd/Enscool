using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Commands.RestoreInstitutionUser;

namespace Modules.Management.Api.Endpoints.InstitutionUsers;

/// <summary>
/// Restore user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class RestoreInstitutionUserEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPatchEndpoint(
                "{userId:guid}",
                ManagementEndpointInfo.InstitutionUsers,
                async (Guid userId, ISender sender) =>
                {
                    var result = await sender.Send(new RestoreInstitutionUserCommand { UserId = userId });
                    return BuildEnvelope(result);
                })
            .RequireInstitutionRoles()
            .ProducesEnvelope(StatusCodes.Status200OK)
            .WithDocumentation(
                "RestoreInstitutionUser",
                "Restore Institution user",
                "Restores Institution user");
    }
}