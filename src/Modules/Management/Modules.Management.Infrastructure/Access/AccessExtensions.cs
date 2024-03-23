using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application.Abstractions.Access;

namespace Modules.Management.Infrastructure.Access;

internal static class AccessExtensions
{
    public static IServiceCollection AddAccess(this IServiceCollection services)
    {
        services.AddSingleton<IRefreshTokenStore, RefreshTokenStore>();
        services.AddScoped<IAccessTokenStore, AccessTokenStore>();
        services.AddScoped<ITokenManager, TokenManager>();

        return services;
    }
}