using Common.Utilities.Primitives.Results;
using MediatR;

namespace Core.Application.Communication.Internal.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;