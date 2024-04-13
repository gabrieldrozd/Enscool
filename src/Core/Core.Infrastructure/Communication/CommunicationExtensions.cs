using System.Reflection;
using Core.Application.Communication.External.Messages;
using Core.Infrastructure.Communication.External.Messages;
using Core.Infrastructure.Communication.Internal;
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

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true, lifetime: ServiceLifetime.Transient);

        services.AddTransient<IMessageBus, MessageBus>();

        return services;
    }

    public static IApplicationBuilder UseCommunication(this WebApplication app)
    {
        return app;
    }
}