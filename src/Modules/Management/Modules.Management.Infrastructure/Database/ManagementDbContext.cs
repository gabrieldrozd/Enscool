using Core.Application.Auth;
using Core.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Database;

public class ManagementDbContext : ApplicationDbContext, IManagementDbContext
{
    public override bool Enabled => true;
    protected override string Schema => "Management";

    #region DbSets

    public DbSet<User> Users => Set<User>();

    #endregion

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