using Core.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application.Abstractions;

namespace Modules.Management.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDatabaseContext<ManagementDbContext>();
        services.AddUnitOfWork<IUnitOfWork, ManagementUnitOfWork>();

        return services;
    }
}