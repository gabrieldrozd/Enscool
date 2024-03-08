using Common.Utilities.Primitives.Results;
using MediatR;

namespace Core.Application.Communication.Queries.Base;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;