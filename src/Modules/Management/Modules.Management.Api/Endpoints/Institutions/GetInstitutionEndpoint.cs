using Common.Utilities.Primitives.Results;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Infrastructure.Modules.Endpoints;
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
                ManagementEndpointInfo.Institutions,
                "{institutionId}",
                async (Guid institutionId) =>
                {
                    var result = await Sender.Send(new GetInstitutionQuery(institutionId));
                    return BuildEnvelope(result);
                })
            // .RequireRoles(UserRole.InstitutionAdmin)
            .ProducesEnvelope<Institution>(StatusCodes.Status200OK)
            .WithDocumentation(
                "GetInstitution",
                "Get institution",
                "Gets the institution by the specified identifier.",
                "Institution");
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