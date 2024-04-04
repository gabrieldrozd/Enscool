using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Database;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseInitializer> _logger;

    public DatabaseInitializer(IServiceProvider serviceProvider, ILogger<DatabaseInitializer> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var contextTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => typeof(ApplicationDbContext).IsAssignableFrom(x) && !x.IsInterface && x != typeof(ApplicationDbContext));

        using var scope = _serviceProvider.CreateScope();
        foreach (var contextType in contextTypes)
        {
            if (scope.ServiceProvider.GetService(contextType) is not ApplicationDbContext context)
                continue;

            if (!context.Enabled)
                continue;

            try
            {
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync(cancellationToken);
                _logger.LogInformation("Applying migrations for '{DbContextName}'", contextType.Name);

                if (pendingMigrations.Any())
                {
                    _logger.LogInformation("Migration(s) to apply:");
                    var migrations = pendingMigrations.Select(x => $"- '{x}',").Aggregate((x, y) => $"{x}{Environment.NewLine}{y}").TrimEnd(',');
                    _logger.LogInformation("{Migrations}", migrations);
                }
                else
                {
                    _logger.LogInformation("No migration(s) to apply");
                }

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