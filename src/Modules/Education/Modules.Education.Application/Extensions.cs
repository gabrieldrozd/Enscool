using Core.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Education.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEducationQueryService, EducationQueryService>();

        return services;
    }

    public static IApplicationBuilder UseApplication(this WebApplication app)
    {
        return app;
    }
}