using Core.Infrastructure;
using Core.Infrastructure.Modules;
using Enscool.Bootstrapper;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();

builder.Host.UseSerilog((context, loggerConfig)
    => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Host.ConfigureModules();

var assemblies = ProjectLoader.LoadAssemblies(builder.Configuration);
var modules = ProjectLoader.LoadProjects<IModuleBase>(assemblies);

#region services

var services = builder.Services;
services.AddCoreInfrastructure(assemblies, modules, builder.Configuration);

#endregion

#region app

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

app.UseCoreInfrastructure(modules);
logger.LogInformation("Modules: [{ModuleNames}]", string.Join(", ", modules.Select(x => x.Name)));

app.MapGet("/", context => context.Response.WriteAsync(
    $"Enscool API is running!\nGo to: {app.Urls.Select(x => x).First()}/docs")
);

#endregion

assemblies.Clear();
modules.Clear();
app.Run();

namespace Enscool.Bootstrapper
{
    public abstract class Program;
}