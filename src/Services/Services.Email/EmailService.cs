using Core.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Email.Queue;
using Services.Email.Sender;
using Services.Email.Settings;

namespace Services.Email;

public class EmailService : IServiceBase
{
    public void RegisterService(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

        services.AddSingleton<IEmailQueue, EmailQueue>();
        services.AddHostedService<EmailSenderBackgroundService>();
        services.AddTransient<IEmailSender, EmailSender>();
    }

    public void UseService(WebApplication app)
    {
    }
}