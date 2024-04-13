using Core.Application.Auth;
using Core.Infrastructure.Database;
using Core.Infrastructure.Database.Converters;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Services.Outbox.OutboxMessages;

namespace Services.Outbox.Database;

public sealed class OutboxDbContext : ApplicationDbContext
{
    public override bool Enabled => true;
    protected override string Schema => $"__{nameof(OutboxDbContext)}";

    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    public OutboxDbContext(DbContextOptions<OutboxDbContext> options, IUserContext userContext, IMediator mediator)
        : base(options, userContext, mediator)
    {
    }

    public override Task InitializeDataAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}