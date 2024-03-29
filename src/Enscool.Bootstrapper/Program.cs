using Core.Infrastructure;
using Core.Infrastructure.Communication.Internal;
using Core.Infrastructure.Cores.Modules;
using Core.Infrastructure.Cores.Services;
using Enscool.Bootstrapper;
using FluentValidation;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Host.UseSerilog((context, loggerConfig)
    => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Host.ConfigureModuleCores();
builder.Host.ConfigureServiceCores();

var assemblies = ProjectLoader.LoadAssemblies(builder.Configuration);
var appModules = ProjectLoader.LoadProjects<IModuleCore>(assemblies);
var appServices = ProjectLoader.LoadProjects<IServiceCore>(assemblies);

#region services

var services = builder.Services;

services.AddCoreInfrastructure(assemblies, builder.Configuration);
foreach (var module in appModules) module.RegisterModule(services, builder.Configuration);
foreach (var service in appServices) service.RegisterService(services, builder.Configuration);

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
});

services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true, lifetime: ServiceLifetime.Transient);
services.Decorate<IMediator, MediatorTransactionDecorator>();

#endregion

#region app

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.UseCoreInfrastructure();
logger.LogInformation("Modules: [{ModuleNames}]", string.Join(", ", appModules.Select(x => $"{x.Name}Module")));
logger.LogInformation("Services: [{ServiceNames}]", string.Join(", ", appServices.Select(x => $"{x.Name}Service")));

appModules.ForEach(x => x.UseRegisteredModule(app));
appServices.ForEach(x => x.UseRegisteredService(app));

app.MapGet("/", context
    => context.Response.WriteAsync(
        $"""
         Enscool API is running!
         Go to: {app.Urls.Select(x => x).First()}/docs
         """
    ));

#endregion

assemblies.Clear();
appModules.Clear();
app.Run();