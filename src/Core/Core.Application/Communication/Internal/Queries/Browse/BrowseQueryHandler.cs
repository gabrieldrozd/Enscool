using Common.Utilities.Primitives.Results;
using Core.Application.Communication.Internal.Queries.Base;
using Core.Application.Queries.Browse;

namespace Core.Application.Communication.Internal.Queries.Browse;

public abstract class BrowseQueryHandler<TBrowseQuery, TResponse> : IQueryHandler<TBrowseQuery, BrowseResult<TResponse>>
    where TBrowseQuery : BrowseQuery<TResponse>
    where TResponse : class
{
    public abstract Task<Result<BrowseResult<TResponse>>> Handle(TBrowseQuery request, CancellationToken cancellationToken);
}