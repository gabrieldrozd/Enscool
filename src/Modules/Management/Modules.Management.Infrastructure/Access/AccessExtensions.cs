using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application.Abstractions.Access;

namespace Modules.Management.Infrastructure.Access;

internal static class AccessExtensions
{
    public static IServiceCollection AddAccess(this IServiceCollection services)
    {
        services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
        services.AddScoped<IRefreshTokenStore, RefreshTokenStore>();

        return services;
    }
}