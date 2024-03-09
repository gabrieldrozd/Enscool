using Core.Infrastructure;
using Core.Infrastructure.Cores.Modules;
using Core.Infrastructure.Cores.Services;
using Enscool.Bootstrapper;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Host.UseSerilog((context, loggerConfig)
    => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Host.ConfigureModuleCores();
builder.Host.ConfigureServiceCores();

var assemblies = ProjectLoader.LoadAssemblies(builder.Configuration);
var appServices = ProjectLoader.LoadProjects<IServiceCore>(assemblies);
var appModules = ProjectLoader.LoadProjects<IModuleCore>(assemblies);

#region services

var services = builder.Services;
services.AddCoreInfrastructure(assemblies, appModules, appServices, builder.Configuration);

#endregion

#region app

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.UseCoreInfrastructure(appModules, appServices);
logger.LogInformation("Modules: [{ModuleNames}]", string.Join(", ", appModules.Select(x => x.Name)));
logger.LogInformation("Services: [{ServiceNames}]", string.Join(", ", appServices.Select(x => x.Name)));

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