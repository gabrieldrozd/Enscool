using Common.Utilities.Primitives.Results;
using MediatR;

namespace Core.Application.Communication.Internal.Queries.Base;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;