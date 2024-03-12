using Core.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Infrastructure.Database.Repositories;

namespace Modules.Management.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDatabaseContext<ManagementDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddUnitOfWork<IUnitOfWork, ManagementUnitOfWork>();

        return services;
    }
}