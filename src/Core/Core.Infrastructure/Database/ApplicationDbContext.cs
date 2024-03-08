using Core.Application.Auth;
using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Database;

public abstract class ApplicationDbContext : DbContext
{
    public abstract bool Enabled { get; }

    /// <summary>
    /// The schema to use by default for all entities in this context.
    /// </summary>
    /// <remarks>
    /// Schema will appear in the database as [Schema].[TableName].
    /// </remarks>
    protected abstract string Schema { get; }

    private readonly IUserContext _userContext;
    private readonly IPublisher _publisher;

    protected ApplicationDbContext(DbContextOptions options, IUserContext userContext, IPublisher publisher)
        : base(options)
    {
        _userContext = userContext;
        _publisher = publisher;
    }

    /// <summary>
    /// Initializes the database with data.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <remarks>
    /// Use specific DbContext to seed data.
    /// </remarks>
    public abstract Task InitializeDataAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Configures the schema to use by default for all entities in this context and applies configurations from the assembly.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    /// <remarks>
    /// There is no need to override this method. Use the <see cref="Schema"/> property instead.
    /// </remarks>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (!string.IsNullOrWhiteSpace(Schema))
            modelBuilder.HasDefaultSchema(Schema);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities(Date.UtcNow, _userContext.UserId);
        await PublishDomainEvents(cancellationToken);
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Updates the entities implementing <see cref="IEntity"/> interface.
    /// </summary>
    /// <param name="utcNow">The current date and time in UTC format.</param>
    /// <param name="userId">The user identifier.</param>
    private void UpdateAuditableEntities(Date utcNow, UserId? userId = null)
    {
        foreach (var entityEntry in ChangeTracker.Entries<IEntity>())
        {
            if (entityEntry.State == EntityState.Added)
                entityEntry.Entity.SetCreated(utcNow, userId);

            if (entityEntry.State == EntityState.Modified)
                entityEntry.Entity.SetModified(utcNow, userId);
        }
    }

    /// <summary>
    /// Publishes and then clears all the domain events that exist within the current transaction.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        var aggregateRoots = ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(entityEntry => entityEntry.Entity.DomainEvents.Any())
            .ToList();

        var domainEvents = aggregateRoots.SelectMany(entityEntry => entityEntry.Entity.DomainEvents).ToList();
        aggregateRoots.ForEach(entityEntry => entityEntry.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);
    }
}