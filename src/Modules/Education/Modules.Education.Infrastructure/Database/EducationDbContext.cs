using Core.Application.Auth;
using Core.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Education.Application.Abstractions;
using Modules.Education.Domain.Students;

namespace Modules.Education.Infrastructure.Database;

public class EducationDbContext : ApplicationDbContext, IEducationDbContext
{
    public override bool Enabled => true;
    protected override string Schema => "Education";

    #region DbSets

    public DbSet<Student> Students => Set<Student>();

    #endregion

    public EducationDbContext(
        DbContextOptions<EducationDbContext> options,
        IUserContext userContext,
        IPublisher publisher
    )
        : base(options, userContext, publisher)
    {
    }

    public override Task InitializeDataAsync(CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}