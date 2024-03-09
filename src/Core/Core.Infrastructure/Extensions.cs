using System.Reflection;
using Core.Infrastructure.Auth;
using Core.Infrastructure.Auth.Contexts;
using Core.Infrastructure.Communication;
using Core.Infrastructure.Cores.Modules;
using Core.Infrastructure.Cores.Services;
using Core.Infrastructure.Database;
using Core.Infrastructure.Middlewares;
using Core.Infrastructure.Security;
using Core.Infrastructure.Services;
using Core.Infrastructure.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddCoreInfrastructure(
        this IServiceCollection services,
        IList<Assembly> assemblies,
        List<IModuleCore> appModules,
        List<IServiceCore> appServices,
        IConfiguration configuration)
    {
        services
            .AddSecurity()
            .AddMiddlewares()
            .AddAuth()
            .AddContexts()
            .AddModuleCores(assemblies)
            .AddServiceCores(assemblies)
            .AddDatabase(assemblies)
            .AddCommunication(assemblies)
            .AddValidations(assemblies)
            .AddServices(assemblies);

        // Register services
        appServices.ForEach(service => service.RegisterService(services, configuration));

        // Register modules
        appModules.ForEach(module => module.RegisterModule(services, configuration));

        return services;
    }

    public static IApplicationBuilder UseCoreInfrastructure(this WebApplication app, List<IModuleCore> appModules, List<IServiceCore> appServices)
    {
        app.UseSecurity();
        app.UseRegisteredMiddleware();
        app.UseModuleCores();
        app.UseServiceCores();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCommunication();
        app.UseValidations();
        app.UseServices();

        // Use services
        appServices.ForEach(service => service.UseService(app));

        // Use modules
        appModules.ForEach(module => module.UseModule(app));

        return app;
    }
}