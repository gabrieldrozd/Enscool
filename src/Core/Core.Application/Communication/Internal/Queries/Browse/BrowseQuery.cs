using Core.Application.Communication.Internal.Queries;
using Core.Application.Queries.Browse;
using Core.Application.Queries.Browse.Pagination;

namespace Wisse.Core.Application.Abstractions.Communication.Internal.Queries.Browse;

public abstract record BrowseQuery<TResponse> : IQuery<BrowseResult<TResponse>>
    where TResponse : class
{
    public BrowseModel Model { get; init; } = new(PaginationModel.Default);
}