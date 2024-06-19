using Core.Application.Communication.Internal.Queries.Base;
using Core.Application.Queries.Browse;

namespace Core.Application.Communication.Internal.Queries.Browse;

public abstract record BrowseQuery<TResponse> : IQuery<BrowseResult<TResponse>>
    where TResponse : class
{
    public BrowseModel? Model { get; init; }
}