using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Validations;

public static class ValidationExtensions
{
    public static IServiceCollection AddValidations(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);

        return services;
    }

    public static IApplicationBuilder UseValidations(this WebApplication app)
    {
        return app;
    }
}