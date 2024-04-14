using Core.Application.Communication.External.Messages;
using Core.Infrastructure.Cores.Services;
using Core.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Outbox.Database;

namespace Services.Outbox;

public class OutboxServiceCore : IServiceCore
{
    public string Name => "Outbox";

    public void RegisterService(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabaseContext<OutboxDbContext>();

        services.AddHostedService<OutboxMessageProcessorJob>();
        services.AddScoped<IMessageBus, MessageBus>();
    }

    public void UseService(WebApplication app)
    {
    }
}