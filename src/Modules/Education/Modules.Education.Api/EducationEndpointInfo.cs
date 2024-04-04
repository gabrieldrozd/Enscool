using Core.Infrastructure.Cores.Modules.Endpoints;
using Core.Infrastructure.Cores.Modules.Swagger.Settings;

namespace Modules.Education.Api;

public sealed record EducationEndpointInfo : EndpointInfo
{
    public static readonly EducationEndpointInfo Students = new(nameof(Students), ApiGroups.Education, "students");

    private EducationEndpointInfo(string value, string modulePath, string route) : base(value, modulePath, route)
    {
    }
}