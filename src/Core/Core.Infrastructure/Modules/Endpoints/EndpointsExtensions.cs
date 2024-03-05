using System.Reflection;
using Carter;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Modules.Endpoints;

public static class EndpointsExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddCarter();
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static IApplicationBuilder UseEndpoints(this WebApplication app)
    {
        app.MapCarter();

        return app;
    }
}