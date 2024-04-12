using Core.Application.Auth;
using Core.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Outbox.OutboxMessages;

namespace Services.Outbox;

public sealed class OutboxDbContext : ApplicationDbContext
{
    public override bool Enabled => true;
    protected override string Schema => $"__{nameof(OutboxDbContext)}";

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public OutboxDbContext(DbContextOptions<OutboxDbContext> options, IUserContext userContext, IMediator mediator)
        : base(options, userContext, mediator)
    {
    }

    // TODO: Add migrations for OutboxDbContext

    public override Task InitializeDataAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}