using Common.Utilities.Primitives.Results;
using MediatR;

namespace Core.Application.Communication.Internal.Queries.Base;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;