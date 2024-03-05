using Core.Infrastructure.Modules;
using Core.Infrastructure.Modules.Swagger.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Management.Api;

internal class ManagementModule : IModuleBase
{
    private const string BaseName = ApiSettings.Management;
    public const string BasePath = ApiGroups.Management;

    public static string Name => BaseName;
    public string Path => BasePath;

    public void RegisterModule(IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Add ManagementModule configuration

        services
            .AddInfrastructure(configuration)
            .AddApplication(configuration);
    }

    public void UseModule(WebApplication app)
    {
    }
}