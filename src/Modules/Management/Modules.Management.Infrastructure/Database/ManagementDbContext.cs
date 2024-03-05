using Core.Application.Abstractions.Auth;
using Core.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Modules.Management.Infrastructure.Database;

public class ManagementDbContext : ApplicationDbContext
{
    public override bool Enabled => true;
    protected override string Schema => "Management";

    public ManagementDbContext(
        DbContextOptions<ManagementDbContext> options,
        IUserContext userContext,
        IPublisher publisher
    )
        : base(options, userContext, publisher)
    {
    }

    public override Task InitializeDataAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}