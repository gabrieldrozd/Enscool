using Core.Infrastructure.Cores.Modules.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Cores.Modules;

public interface IModuleCore
{
    string Name { get; }
    string Path { get; }

    void RegisterModule(IServiceCollection services, IConfiguration configuration);

    void UseModule(WebApplication app);

    public void UseRegisteredModule(WebApplication app)
    {
        UseModule(app);
        AddEndpoints(app);
    }

    public void AddEndpoints(WebApplication app)
    {
        var assembly = GetType().Assembly;
        var endpointTypes = assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(EndpointBase)) && !type.IsAbstract)
            .Select(type => ActivatorUtilities.CreateInstance(app.Services, type) as EndpointBase)
            .Where(endpoint => endpoint is not null)
            .ToList();

        endpointTypes.ForEach(endpoint => endpoint!.AddEndpoint(app));
    }
}