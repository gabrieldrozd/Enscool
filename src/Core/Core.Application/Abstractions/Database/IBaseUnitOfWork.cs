using Common.Utilities.Primitives.Results;

namespace Core.Application.Abstractions.Database;

public interface IBaseUnitOfWork
{
    Task<Result> CommitAsync(CancellationToken cancellationToken = default);
}