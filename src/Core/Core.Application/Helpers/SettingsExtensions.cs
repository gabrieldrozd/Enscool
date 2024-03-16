using Common.Utilities.Abstractions;
using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Helpers;

public static class SettingsExtensions
{
    public static TSettings RegisterSettings<TSettings>(this IServiceCollection services, string sectionName)
        where TSettings : class, ISettings, new()
    {
        Ensure.Not.NullOrEmpty(sectionName);

        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var section = configuration.GetSection(sectionName);
        services.Configure<TSettings>(section);
        var settings = section.Get<TSettings>();
        return settings ?? throw new ConfigurationException($"Settings not found for section '{sectionName}'.");
    }

    public static TSettings GetSettings<TSettings>(this IServiceCollection services, string sectionName)
        where TSettings : class, ISettings, new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.BindSettings<TSettings>(sectionName);
    }

    private static TSettings BindSettings<TSettings>(this IConfiguration configuration, string sectionName)
        where TSettings : class, ISettings, new()
    {
        var settings = new TSettings();
        configuration.GetSection(sectionName).Bind(settings);
        return settings;
    }
}