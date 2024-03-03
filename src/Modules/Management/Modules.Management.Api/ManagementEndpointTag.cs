using Core.Infrastructure.Abstractions.Modules.Endpoints;

namespace Modules.Management.Api;

public sealed record ManagementEndpointTag : EndpointTag
{
    public static readonly ManagementEndpointTag Institutions = new(nameof(Institutions), "institutions");

    private ManagementEndpointTag(string value, string route) : base(value, route)
    {
    }
}