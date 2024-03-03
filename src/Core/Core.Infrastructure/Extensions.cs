using System.Reflection;
using Core.Infrastructure.Abstractions.Modules;
using Core.Infrastructure.Auth;
using Core.Infrastructure.Auth.Contexts;
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
        List<string> disabledModules = [];
        using (var serviceProvider = services.BuildServiceProvider())
        {
            var config = serviceProvider.GetRequiredService<IConfiguration>();
            foreach (var (key, value) in config.AsEnumerable())
            {
                if (!key.Contains(":module:enabled"))
                    continue;

                if (!bool.Parse(value!))
                    disabledModules.Add(key.Split(":")[0]);
            }
        }

        services.AddCors(cors =>
        {
            cors.AddPolicy(CorsPolicy, builder =>
            {
                builder
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        services.RegisterMiddlewares();
        services.RegisterAuth();
        services.RegisterContexts();

        return services;
    }

    public static IApplicationBuilder UseCoreInfrastructure(this WebApplication app)
    {
        app.UseCors(CorsPolicy);
        app.UseRegisteredMiddleware();

        // TODO: UseModulesConfiguration goes here

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }
}