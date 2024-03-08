using Common.Utilities.Primitives.Results;

namespace Core.Application.Database;

public interface IBaseUnitOfWork
{
    Task<Result> CommitAsync(CancellationToken cancellationToken = default);
}