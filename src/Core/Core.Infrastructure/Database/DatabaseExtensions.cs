using Core.Application.Abstractions.Auth;
using Core.Application.Abstractions.Database;
using Core.Application.Helpers;
using Core.Infrastructure.Database.Interceptors;
using Core.Infrastructure.Database.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Database;

public static class DatabaseExtensions
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        var options = services.GetSettings<DatabaseSettings>(DatabaseSettings.SectionName);
        services.AddSingleton(options);
        services.AddSingleton(new UnitOfWorkTypeRegistry());

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }

    public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services, string? connectionString = null)
        where T : DbContext
    {
        var settings = services.GetSettings<DatabaseSettings>(DatabaseSettings.SectionName);
        services.AddDbContext<T>((sp, options) =>
        {
            options.UseNpgsql(connectionString ?? settings.ConnectionString);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();

            options.AddInterceptors(new EntityDeletedSaveChangesInterceptor(sp.GetRequiredService<IUserContext>()));
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    public static IServiceCollection AddUnitOfWork<TUnitOfWork, TImplementation>(this IServiceCollection services)
        where TUnitOfWork : class, IBaseUnitOfWork
        where TImplementation : class, TUnitOfWork
    {
        services.AddScoped<TUnitOfWork, TImplementation>();
        services.AddScoped<IBaseUnitOfWork, TImplementation>();

        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>()
            .Register<TUnitOfWork>();

        return services;
    }
}