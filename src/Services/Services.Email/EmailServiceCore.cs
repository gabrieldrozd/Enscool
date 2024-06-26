using Core.Application.Communication.External.Emails;
using Core.Infrastructure.Cores.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Email.Sender;

namespace Services.Email;

public class EmailServiceCore : IServiceCore
{
    public string Name => "Email";

    public void RegisterService(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

        services.AddSingleton<IEmailQueue, EmailQueue>();
        services.AddHostedService<EmailSenderBackgroundService>();
        services.AddTransient<IEmailSender, EmailSender>();
    }

    // TODO: Change the `Services` to `Components`
    // TODO: Change the `Services` to `Components`
    // TODO: Change the `Services` to `Components`
    // TODO: Change the `Services` to `Components`
    // TODO: Change the `Services` to `Components`

    public void UseService(WebApplication app)
    {
    }
}