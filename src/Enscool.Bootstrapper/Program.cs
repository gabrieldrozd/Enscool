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

services.AddValidatorsFromAssemblies(assemblies, includeInternalTypes: true, lifetime: ServiceLifetime.Scoped);

services.AddCoreInfrastructure(assemblies, builder.Configuration);
appModules.ForEach(module => module.RegisterModule(services, builder.Configuration));
appServices.ForEach(service => service.RegisterService(services, builder.Configuration));

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies.ToArray());
    // cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    // cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    // cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
});

#endregion

#region app

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.UseCoreInfrastructure();
logger.LogInformation("Modules: [{ModuleNames}]", string.Join(", ", appModules.Select(x => $"{x.Name}Module")));
logger.LogInformation("Services: [{ServiceNames}]", string.Join(", ", appServices.Select(x => $"{x.Name}Service")));

appModules.ForEach(module => module.UseModule(app));
appServices.ForEach(service => service.UseService(app));

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