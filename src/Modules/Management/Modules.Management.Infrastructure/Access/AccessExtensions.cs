using Core.Application.Helpers;
using Core.Infrastructure.Auth;
using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application.Abstractions.Access;

namespace Modules.Management.Infrastructure.Access;

internal static class AccessExtensions
{
    public static IServiceCollection AddAccess(this IServiceCollection services)
    {
        services.AddScoped<ITokenProvider, TokenProvider>();
        services.AddScoped<ITokenManager, TokenManager>();

        return services;
    }
}