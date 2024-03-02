using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.Helpers;

public static class SettingsExtensions
{
    public static T GetSettings<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.BindSettings<T>(sectionName);
    }

    private static T BindSettings<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var settings = new T();
        configuration.GetSection(sectionName).Bind(settings);
        return settings;
    }
}