using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Communication;

public static class CommunicationExtensions
{
    public static IServiceCollection AddCommunication(this IServiceCollection services, IList<Assembly> assemblies)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
            // TODO: cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            // TODO: cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(InstitutionAccessBehavior<,>));
            // TODO: cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true);
        // TODO: MediatR Transactional decorator configuration goes here

        return services;
    }

    public static IApplicationBuilder UseCommunication(this WebApplication app)
    {
        return app;
    }
}