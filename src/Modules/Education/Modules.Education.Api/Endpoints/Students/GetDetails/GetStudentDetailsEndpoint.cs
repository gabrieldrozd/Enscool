using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Education.Application.Features.Students.Queries.GetStudent;

namespace Modules.Education.Api.Endpoints.Students.GetDetails;

/// <summary>
/// Get current user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class GetStudentDetailsEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                "{institutionId:guid}",
                EducationEndpointInfo.Students,
                async (Guid institutionId, ISender sender) =>
                {
                    var result = await sender.Send(new GetStudentDetailsQuery(institutionId));
                    return BuildEnvelope(result);
                })
            .RequireAuthorization()
            .RequireInstitutionRoles()
            .ProducesEnvelope<GetStudentDetailsQueryDto>(StatusCodes.Status200OK)
            .WithDocumentation(
                "GetStudentDetails",
                "Get students details",
                "Gets details of the students by its id.");
    }
}