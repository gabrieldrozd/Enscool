using Core.Infrastructure.Cores.Modules;
using Core.Infrastructure.Cores.Modules.Swagger.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application;
using Modules.Management.Infrastructure;

namespace Modules.Management.Api;

internal class ManagementModule : IModuleCore
{
    public string Name => ApiSettings.Management;
    public string Path => ApiGroups.Management;

    public void RegisterModule(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructure()
            .AddApplication();
    }

    public void UseModule(WebApplication app)
    {
        app.UseInfrastructure();
        app.UseApplication();
    }
}