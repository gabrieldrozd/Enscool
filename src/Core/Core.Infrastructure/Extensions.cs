using System.Reflection;
using Core.Infrastructure.Auth;
using Core.Infrastructure.Auth.Contexts;
using Core.Infrastructure.Communication;
using Core.Infrastructure.Database;
using Core.Infrastructure.Middlewares;
using Core.Infrastructure.Modules;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure;

internal static class Extensions
{
    private const string CorsPolicy = "cors-policy";

    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, IList<Assembly> assemblies, List<IModuleBase> modules, IConfiguration configuration)
    {
        services
            .AddCors(cors =>
            {
                cors.AddPolicy(CorsPolicy, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            })
            .AddMiddlewares()
            .AddAuth()
            .AddContexts()
            .AddModules(assemblies)
            .AddDatabase()
            .AddCommunication(assemblies);

        // Register modules
        modules.ForEach(module => module.RegisterModule(services, configuration));

        return services;
    }

    public static IApplicationBuilder UseCoreInfrastructure(this WebApplication app, List<IModuleBase> modules)
    {
        app.UseCors(CorsPolicy);
        app.UseRegisteredMiddleware();
        app.UseModules();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();
        app.UseCommunication();

        // Use modules
        modules.ForEach(module => module.UseModule(app));

        return app;
    }
}