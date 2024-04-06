using Core.Infrastructure.Cores.Modules;
using Core.Infrastructure.Cores.Modules.Swagger.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Education.Application;
using Modules.Education.Infrastructure;

namespace Modules.Education.Api;

internal class EducationModule : IModuleCore
{
    public string Name => ApiSettings.Education;
    public string Path => ApiGroups.Education;

    public void RegisterModule(IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddInfrastructure(configuration)
            .AddApplication(configuration);
    }

    public void UseModule(WebApplication app)
    {
        app.UseInfrastructure();
        app.UseApplication();
    }
}