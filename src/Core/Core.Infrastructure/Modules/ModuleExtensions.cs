using System.Reflection;
using Core.Infrastructure.Modules.Endpoints;
using Core.Infrastructure.Modules.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Infrastructure.Modules;

/// <summary>
/// Extension methods for configuring modules.
/// </summary>
public static class ModuleExtensions
{
    internal static IServiceCollection AddModules(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services
            .AddSwaggerDocumentation(assemblies)
            .AddEndpoints(assemblies);

        return services;
    }

    internal static IApplicationBuilder UseModules(this WebApplication app)
    {
        app.UseSwaggerDocumentation();
        app.UseEndpoints();

        return app;
    }

    /// <summary>
    /// Configures modules for the host builder by adding JSON configuration files based on the environment.
    /// </summary>
    /// <param name="builder">The host builder.</param>
    /// <returns>The host builder with the modules configured.</returns>
    public static IHostBuilder ConfigureModules(this IHostBuilder builder)
    {
        return builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            foreach (var settings in GetSettings("*", ctx))
                cfg.AddJsonFile(settings);

            foreach (var settings in GetSettings($"*.{ctx.HostingEnvironment.EnvironmentName}", ctx))
                cfg.AddJsonFile(settings);
        });
    }

    private static IEnumerable<string> GetSettings(string pattern, HostBuilderContext ctx) =>
        Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"module.{pattern}.json", SearchOption.AllDirectories);
}