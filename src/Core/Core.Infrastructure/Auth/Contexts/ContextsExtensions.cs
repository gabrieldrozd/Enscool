using Core.Application.Abstractions.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Auth.Contexts;

internal static class ContextsExtensions
{
    public static IServiceCollection RegisterContexts(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddTransient<IUserContext, UserContext>();

        return services;
    }
}