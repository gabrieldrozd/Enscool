using System.Reflection;
using Core.Infrastructure.Auth;
using Core.Infrastructure.Auth.Contexts;
using Core.Infrastructure.Cores.Modules;
using Core.Infrastructure.Cores.Services;
using Core.Infrastructure.Database;
using Core.Infrastructure.Middlewares;
using Core.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddCoreInfrastructure(
        this IServiceCollection services,
        IList<Assembly> assemblies,
        IConfiguration configuration)
    {
        services
            .AddSecurity()
            .AddMiddlewares()
            .AddAuth(configuration)
            .AddContexts()
            .AddModuleCores(assemblies)
            .AddServiceCores(assemblies)
            .AddDatabase(assemblies);

        return services;
    }

    public static IApplicationBuilder UseCoreInfrastructure(this WebApplication app)
    {
        app.UseSecurity();
        app.UseRegisteredMiddleware();
        app.UseModuleCores();
        app.UseServiceCores();
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }
}