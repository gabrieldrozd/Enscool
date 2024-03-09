using Core.Infrastructure.Cores.Modules.Endpoints;
using Core.Infrastructure.Cores.Modules.Swagger.Settings;

namespace Modules.Management.Api;

public sealed record ManagementEndpointInfo : EndpointInfo
{
    public static readonly ManagementEndpointInfo Institutions = new(nameof(Institutions), ApiGroups.Management, "institutions");
    public static readonly ManagementEndpointInfo Users = new(nameof(Users), ApiGroups.Management, "users");

    private ManagementEndpointInfo(string value, string modulePath, string route) : base(value, modulePath, route)
    {
    }
}