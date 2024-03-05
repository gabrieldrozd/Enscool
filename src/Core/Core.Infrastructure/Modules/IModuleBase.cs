using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Modules;

public interface IModuleBase
{
    string Name { get; }
    string Path { get; }

    void RegisterModule(IServiceCollection services, IConfiguration configuration);

    void UseModule(WebApplication app);
}