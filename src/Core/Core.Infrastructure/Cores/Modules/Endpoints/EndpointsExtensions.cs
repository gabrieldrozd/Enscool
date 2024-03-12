using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Cores.Modules.Endpoints;

public static class EndpointsExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static IApplicationBuilder UseEndpoints(this WebApplication app)
    {
        return app;
    }
}