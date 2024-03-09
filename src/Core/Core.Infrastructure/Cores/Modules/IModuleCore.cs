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
}