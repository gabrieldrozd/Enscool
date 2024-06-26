using Common.Utilities.Abstractions.Mapping;
using Core.Application.Files;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Modules.Management.Application.Features.InstitutionUsers.Commands.Export;

namespace Modules.Management.Api.Endpoints.InstitutionUsers;

/// <summary>
/// Export users <see cref="EndpointBase"/>.
/// </summary>
internal sealed class ExportInstitutionUsersEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapPostEndpoint(
                "export",
                ManagementEndpointInfo.InstitutionUsers,
                async ([FromBody] ExportInstitutionUsersRequest request, ISender sender) =>
                {
                    var result = await sender.Send(request.Map());
                    return BuildEnvelope(result);
                })
            .RequireInstitutionRoles()
            .ProducesEnvelope<FileDto>(StatusCodes.Status200OK)
            .WithDocumentation(
                "ExportInstitutionUsers",
                "Export Institution users",
                "Exports Institution users");
    }
}

internal sealed class ExportInstitutionUsersRequest : IWithMapTo<ExportInstitutionUsersCommand>
{
    public Guid InstitutionId { get; init; }
    public List<Guid> UserIds { get; init; } = [];
    public List<string> Columns { get; set; } = [];

    public ExportInstitutionUsersCommand Map() => new()
    {
        InstitutionId = InstitutionId,
        UserIds = UserIds,
        Columns = Columns
    };
}