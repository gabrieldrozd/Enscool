using System.Reflection;
using Common.Utilities.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Services;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IList<Assembly> assemblies)
    {
        #region Repositories

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        #endregion

        #region Services

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        #endregion

        #region Domain Services

        services.Scan(scan => scan
            .FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainService)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        #endregion

        return services;
    }

    public static IApplicationBuilder UseServices(this WebApplication app)
    {
        return app;
    }
}