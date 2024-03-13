using Common.Utilities.Primitives.Results;
using MediatR;

namespace Core.Application.Communication.Internal.Commands;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;