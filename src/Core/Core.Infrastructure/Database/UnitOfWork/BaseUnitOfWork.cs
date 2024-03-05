using Common.Utilities.Primitives.Results;
using Common.Utilities.Resources;
using Core.Application.Abstractions.Database;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Database.UnitOfWork;

public abstract class BaseUnitOfWork<TDbContext> : IBaseUnitOfWork
    where TDbContext : DbContext
{
    private readonly TDbContext _context;

    protected BaseUnitOfWork(TDbContext context)
    {
        _context = context;
    }

    public async Task<Result> CommitAsync(CancellationToken cancellationToken = default)
    {
        bool commitStatus;

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
            commitStatus = true;
        }
        catch (Exception)
        {
            commitStatus = false;
        }

        return commitStatus
            ? Result.Success.Ok()
            : Result.Error.ServerError(Resource.DatabaseError);
    }
}