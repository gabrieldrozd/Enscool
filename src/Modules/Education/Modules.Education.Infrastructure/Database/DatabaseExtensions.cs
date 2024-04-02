using Core.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Infrastructure.Database.Repositories;

namespace Modules.Education.Infrastructure.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDatabaseContext<EducationDbContext>();
        services.AddScoped<IEducationDbContext, EducationDbContext>();

        services.AddScoped<IStudentRepository, StudentRepository>();

        services.AddUnitOfWork<IUnitOfWork, EducationUnitOfWork>();

        return services;
    }
}