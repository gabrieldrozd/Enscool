using Common.Utilities.Abstractions.Mapping;
using Core.Application.Files;
using Core.Domain.Shared.EntityIds;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Commands.DeactivateInstitutionUsers;
using Modules.Management.Application.Features.InstitutionUsers.Commands.ExportInstitutionUsers;

namespace Modules.Management.Api.Endpoints.InstitutionUsers;

/// <summary>
/// Export users <see cref="EndpointBase"/>.
/// </summary>
internal sealed class DeactivateInstitutionUsersEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPatchEndpoint(
                "deactivate",
                ManagementEndpointInfo.InstitutionUsers,
                async ([FromBody] DeactivateInstitutionUsersRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .RequireInstitutionRoles()
            .ProducesEnvelope<FileDto>(StatusCodes.Status200OK)
            .WithDocumentation(
                "DeactivateInstitutionUsers",
                "Deactivate Institution users",
                "Deactivates Institution users");
    }
}

internal sealed class DeactivateInstitutionUsersRequest : IWithMapTo<DeactivateInstitutionUsersCommand>
{
    public List<Guid> UserIds { get; init; } = [];

    public DeactivateInstitutionUsersCommand Map() => new() { UserIds = UserIds };
}