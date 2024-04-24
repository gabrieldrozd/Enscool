using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Education.Application.Abstractions;
using Modules.Education.Infrastructure.Database;
using Modules.Education.Infrastructure.Services;

namespace Modules.Education.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase();

        services.AddScoped<ICourseCodeGenerator, CourseCodeGenerator>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}