using Common.Utilities.Primitives.Results;
using Core.Infrastructure.Cores.Modules.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Modules.Management.Api.Endpoints.Institutions;

public class GetInstitutionEndpoint : EndpointBase
{
    public override void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder
            .MapGetEndpoint(
                "{institutionId}",
                ManagementEndpointInfo.Institutions,
                async (Guid institutionId, ISender sender) =>
                {
                    var result = await sender.Send(new GetInstitutionQuery(institutionId));
                    return BuildEnvelope(result);
                })
            // .RequireRoles(UserRole.InstitutionAdmin)
            .ProducesEnvelope<Institution>(StatusCodes.Status200OK)
            .WithDocumentation(
                "GetInstitution",
                "Get institution",
                "Gets the institution by the specified identifier.");
    }
}

public class Institution
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}

public record GetInstitutionQuery(Guid InstitutionId) : IRequest<Result<Institution>>
{
    internal sealed class Handler : IRequestHandler<GetInstitutionQuery, Result<Institution>>
    {
        public Task<Result<Institution>> Handle(GetInstitutionQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Result.Success.Ok(new Institution
            {
                Id = request.InstitutionId,
                Name = "Institution",
                Description = "Institution description",
                Address = "Institution address",
                City = "Institution city",
                State = "Institution state",
                ZipCode = "Institution zip code",
                Country = "Institution country"
            }));
        }
    }
}