using System.Reflection;
using Core.Infrastructure.Auth;
using Core.Infrastructure.Auth.Contexts;
using Core.Infrastructure.Communication;
using Core.Infrastructure.Database;
using Core.Infrastructure.Middlewares;
using Core.Infrastructure.Modules;
using Core.Infrastructure.Security;
using Core.Infrastructure.Services;
using Core.Infrastructure.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure;

internal static class Extensions
{
    private const string CorsPolicy = "cors-policy";

    public static IServiceCollection AddCoreInfrastructure(
        this IServiceCollection services,
        IList<Assembly> assemblies,
        List<IModuleBase> appModules,
        List<IServiceBase> appServices,
        IConfiguration configuration)
    {
        services
            .AddSecurity()
            .AddMiddlewares()
            .AddAuth()
            .AddContexts()
            .AddModules(assemblies)
            .AddServices(assemblies)
            .AddDatabase()
            .AddCommunication(assemblies)
            .AddValidations(assemblies);

        // Register modules
        appModules.ForEach(module => module.RegisterModule(services, configuration));
        appServices.ForEach(service => service.RegisterService(services, configuration));

        return services;
    }

    public static IApplicationBuilder UseCoreInfrastructure(this WebApplication app, List<IModuleBase> appModules, List<IServiceBase> appServices)
    {
        app.UseSecurity();
        app.UseRegisteredMiddleware();
        app.UseModules();
        app.UseServices();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCommunication();
        app.UseValidations();

        // Use modules
        appModules.ForEach(module => module.UseModule(app));
        appServices.ForEach(service => service.UseService(app));

        return app;
    }
}