using Common.Utilities.Abstractions;
using Common.Utilities.Primitives.Ensures;
using Common.Utilities.Primitives.Ensures.EnsureNotExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Helpers;

public static class SettingsExtensions
{
    /// <summary>
    /// <para>
    /// Tries to get an instance of <typeparamref name="TSettings"/> from the <see cref="IServiceCollection"/>.
    /// </para>
    /// </summary>
    /// <typeparam name="TSettings"><see cref="ISettings"/> type.</typeparam>
    /// <param name="services"><see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="sectionName"><see cref="IConfiguration"/> section name.</param>
    /// <returns>The configured <typeparamref name="TSettings"/> instance.</returns>
    public static TSettings ConfigureSettings<TSettings>(this IServiceCollection services, string sectionName)
        where TSettings : class, ISettings, new()
    {
        Ensure.Not.NullOrEmpty(sectionName);

        var serviceProvider = services.BuildServiceProvider();
        var settings = serviceProvider.GetService<TSettings>();
        if (settings is not null)
            return settings;

        settings = new TSettings();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        configuration.GetSection(sectionName).Bind(settings);
        services.AddSingleton(settings);
        return settings;
    }

    public static TSettings GetSettings<TSettings>(this IServiceCollection services, string sectionName)
        where TSettings : class, new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.BindSettings<TSettings>(sectionName);
    }

    private static TSettings BindSettings<TSettings>(this IConfiguration configuration, string sectionName)
        where TSettings : class, new()
    {
        var settings = new TSettings();
        configuration.GetSection(sectionName).Bind(settings);
        return settings;
    }
}