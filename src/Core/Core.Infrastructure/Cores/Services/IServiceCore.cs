using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Cores.Services;

public interface IServiceCore
{
    string Name => GetType().Name;

    void RegisterService(IServiceCollection services, IConfiguration configuration);

    void UseService(WebApplication app);
}