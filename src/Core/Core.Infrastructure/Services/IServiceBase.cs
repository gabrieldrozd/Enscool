using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Services;

public interface IServiceBase
{
    string Name => GetType().Name;

    void RegisterService(IServiceCollection services, IConfiguration configuration);

    void UseService(WebApplication app);
}