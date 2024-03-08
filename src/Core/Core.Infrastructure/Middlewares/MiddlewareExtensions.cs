using Core.Application.Exceptions;
using Core.Domain.Shared.Enumerations;
using Core.Infrastructure.Middlewares.Culture;
using Core.Infrastructure.Middlewares.Exceptions;
using Core.Infrastructure.Middlewares.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Middlewares;

internal static class MiddlewareExtensions
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddSingleton<IExceptionMapper, ExceptionMapper>();

        services.AddScoped<ExceptionMiddleware>();
        services.AddScoped<CultureMiddleware>();
        services.AddScoped<HttpRequestMiddleware>();

        return services;
    }

    public static IApplicationBuilder UseRegisteredMiddleware(this IApplicationBuilder app)
    {
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(Language.Default)
            .AddSupportedCultures(Language.Cultures.ToArray())
            .AddSupportedUICultures(Language.Cultures.ToArray());

        app.UseRequestLocalization(localizationOptions);
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<CultureMiddleware>();
        app.UseMiddleware<HttpRequestMiddleware>();

        return app;
    }
}