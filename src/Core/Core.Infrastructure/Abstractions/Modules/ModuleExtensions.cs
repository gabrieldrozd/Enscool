using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Core.Infrastructure.Abstractions.Modules;

/// <summary>
/// Extension methods for configuring modules.
/// </summary>
public static class ModuleExtensions
{
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