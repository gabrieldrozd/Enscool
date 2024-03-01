using System.Reflection;
using Core.Infrastructure.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure;

internal static class Extensions
{
    // TODO: Move to a separate extension method
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

        // TODO: Move to a separate extension method
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

        return services;
    }

    public static IApplicationBuilder UseCoreInfrastructure(this WebApplication app)
    {
        // TODO: Move to a separate extension method
        app.UseCors(CorsPolicy);

        // TODO: `app.UseAuthentication();` goes here
        app.UseRouting();
        // TODO: `app.UseAuthorization();` goes here

        return app;
    }
}