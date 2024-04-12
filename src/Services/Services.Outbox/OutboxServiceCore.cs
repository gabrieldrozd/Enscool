using Core.Application.Communication.External.Emails;
using Core.Application.Communication.External.Messages;
using Core.Infrastructure.Cores.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Outbox;

public class OutboxServiceCore : IServiceCore
{
    public string Name => "Outbox";

    public void RegisterService(IServiceCollection services, IConfiguration configuration)
    {
        // services.Configure<OutboxSettings>(configuration.GetSection(OutboxSettings.SectionName));

        services.AddHostedService<OutboxProcessorBackgroundService>();
        services.AddTransient<IOutboxWriter, OutboxWriter>();
    }

    public void UseService(WebApplication app)
    {
    }
}