using System.Reflection;
using Core.Infrastructure.Communication.Internal;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Communication;

public static class CommunicationExtensions
{
    public static IServiceCollection AddCommunication(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies.ToArray());

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>), ServiceLifetime.Scoped);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>), ServiceLifetime.Scoped);
        });

        return services;
    }

    public static IApplicationBuilder UseCommunication(this WebApplication app)
    {
        return app;
    }
}