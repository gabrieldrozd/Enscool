using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Modules.Education.Application.Features.Students.Queries.GetStudent;

namespace Modules.Education.Api.Endpoints.Students;

/// <summary>
/// Get current user <see cref="EndpointBase"/>.
/// </summary>
internal sealed class GetStudentDetailsEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                "{studentId:guid}",
                EducationEndpointInfo.Students,
                async (Guid studentId, ISender sender) =>
                {
                    var result = await sender.Send(new GetStudentDetailsQuery(studentId));
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