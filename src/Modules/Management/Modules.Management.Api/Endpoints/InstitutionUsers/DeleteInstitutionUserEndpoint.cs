using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Commands.Delete;

namespace Modules.Management.Api.Endpoints.InstitutionUsers;

/// <summary>
/// Delete user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class DeleteInstitutionUserEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapDeleteEndpoint(
                "{userId:guid}",
                ManagementEndpointInfo.InstitutionUsers,
                async (Guid userId, ISender sender) =>
                {
                    var result = await sender.Send(new DeleteInstitutionUserCommand { UserId = userId });
                    return BuildEnvelope(result);
                })
            .RequireInstitutionRoles()
            .ProducesEnvelope(StatusCodes.Status200OK)
            .WithDocumentation(
                "DeleteInstitutionUser",
                "Delete Institution user",
                "Deletes Institution user");
    }
}