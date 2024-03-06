using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Security;

public static class SecurityExtensions
{
    private const string CorsPolicy = "cors-policy";

    public static IServiceCollection AddSecurity(this IServiceCollection services)
    {
        services
            .AddCors(cors =>
            {
                cors.AddPolicy(CorsPolicy, builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

        return services;
    }

    public static IApplicationBuilder UseSecurity(this WebApplication app)
    {
        app.UseCors(CorsPolicy);

        return app;
    }
}