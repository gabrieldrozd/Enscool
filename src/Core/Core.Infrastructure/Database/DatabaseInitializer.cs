using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Database;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IServiceScopeFactory serviceScopeFactory, ILogger<DatabaseInitializer> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var contextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(ApplicationDbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(ApplicationDbContext));

        using var scope = _serviceScopeFactory.CreateScope();
        foreach (var contextType in contextTypes)
        {
            if (scope.ServiceProvider.GetService(contextType) is not ApplicationDbContext context)
                continue;

            if (!context.Enabled)
                continue;

            try
            {
                _logger.LogInformation("Applying migrations for '{DbContextName}'", contextType.Name);
                await context.Database.MigrateAsync(cancellationToken);

                _logger.LogInformation("Initializing database for '{DbContextName}'", contextType.Name);
                await context.InitializeDataAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error occurred while migrating the database for '{DbContextName}'", contextType.Name);
            }

            _logger.LogInformation("Finished migrating and initializing database for '{DbContextName}'", contextType.Name);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}