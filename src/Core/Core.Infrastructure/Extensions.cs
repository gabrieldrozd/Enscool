using System.Reflection;
using Core.Infrastructure.Abstractions.Modules;
using Core.Infrastructure.Auth;
using Core.Infrastructure.Auth.Contexts;
using Core.Infrastructure.Database;
using Core.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure;

internal static class Extensions
{
    private const string CorsPolicy = "cors-policy";

    public static IServiceCollection AddCoreInfrastructure(this IServiceCollection services, IList<Assembly> assemblies, IList<IModuleBase> modules, IConfiguration configuration)
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
            .AddDatabase();

        return services;
    }

    public static IApplicationBuilder UseCoreInfrastructure(this WebApplication app)
    {
        app.UseCors(CorsPolicy);
        app.UseRegisteredMiddleware();
        app.UseModules();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }
}