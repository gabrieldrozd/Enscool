using Core.Application.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Core.Infrastructure.Storage;

internal static class StorageExtensions
{
    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        var settings = services.RegisterSettings<StorageSettings>(StorageSettings.SectionName);

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(settings.ConnectionString));

        return services;
    }
}