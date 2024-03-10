using Core.Application.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application.Abstractions.Settings;
using Modules.Management.Application.Services;
using Modules.Management.Domain.Abstractions;

namespace Modules.Management.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.ConfigureSettings<AccountActivationSettings>(AccountActivationSettings.SectionName);
        services.AddTransient<IActivationCodeService, ActivationCodeService>();
        services.AddTransient<IActivationLinkService, ActivationLinkService>();

        return services;
    }

    public static IApplicationBuilder UseApplication(this WebApplication app)
    {
        return app;
    }
}