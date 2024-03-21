using Core.Application.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application.Abstractions.Settings;
using Modules.Management.Application.Services;
using Modules.Management.Domain.Abstractions;

namespace Modules.Management.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterSettings<AccountActivationSettings>(AccountActivationSettings.SectionName);
        services.RegisterSettings<PasswordResetSettings>(PasswordResetSettings.SectionName);

        services.AddScoped<IActivationCodeService, ActivationCodeService>();
        services.AddScoped<IActivationLinkService, ActivationLinkService>();

        return services;
    }

    public static IApplicationBuilder UseApplication(this WebApplication app)
    {
        return app;
    }
}