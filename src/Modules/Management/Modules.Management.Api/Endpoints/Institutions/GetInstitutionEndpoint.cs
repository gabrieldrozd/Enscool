using Common.Utilities.Primitives.Results;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Infrastructure.Abstractions.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Institutions;

public class GetInstitutionEndpoint : EndpointBase
{
    public GetInstitutionEndpoint(ISender sender) : base(sender)
    {
    }

    public override void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                ManagementEndpointTag.Institutions,
                "/institutions/{institutionId}",
                async (Guid institutionId) =>
                {
                    var result = await Sender.Send(new GetInstitutionQuery(institutionId));
                    return BuildEnvelope(result);
                })
            .RequireRoles(UserRole.InstitutionAdmin)
            .ProducesEnvelope<Institution>(StatusCodes.Status200OK)
            .ProducesEnvelope(StatusCodes.Status404NotFound)
            .WithDocumentation(
                "GetInstitution",
                "Get institution",
                "Gets the institution by the specified identifier.",
                "Institution");
    }
}

public class Institution
{
}

public class GetInstitutionQuery : IRequest<Result>
{
    public GetInstitutionQuery(Guid institutionId)
    {
        throw new NotImplementedException();
    }
}