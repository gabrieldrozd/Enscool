using Core.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application.Abstractions;
using Modules.Management.Infrastructure.Database;

namespace Modules.Management.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDatabaseContext<ManagementDbContext>();

        services.AddUnitOfWork<IUnitOfWork, ManagementUnitOfWork>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}