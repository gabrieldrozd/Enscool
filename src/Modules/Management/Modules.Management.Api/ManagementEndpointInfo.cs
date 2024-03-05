using Core.Infrastructure.Modules.Endpoints;
using Core.Infrastructure.Modules.Swagger.Settings;

namespace Modules.Management.Api;

public sealed record ManagementEndpointInfo : EndpointInfo
{
    public static readonly ManagementEndpointInfo Institutions = new(nameof(Institutions), ApiGroups.Management, "institutions");

    private ManagementEndpointInfo(string value, string modulePath, string route) : base(value, modulePath, route)
    {
    }
}