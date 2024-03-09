using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Core.Infrastructure.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IList<Assembly> assemblies)
    {
        return services;
    }

    public static IApplicationBuilder UseServices(this WebApplication app)
    {
        return app;
    }

    /// <summary>
    /// Configures services for the host builder by adding JSON configuration files based on the environment.
    /// </summary>
    /// <param name="builder">The host builder.</param>
    /// <returns>The host builder with the services configured.</returns>
    public static IHostBuilder ConfigureServices(this IHostBuilder builder)
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
        Directory.EnumerateFiles(ctx.HostingEnvironment.ContentRootPath, $"service.{pattern}.json", SearchOption.AllDirectories);
}