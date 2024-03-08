using Common.Utilities.Primitives.Results;
using MediatR;

namespace Core.Application.Communication.Commands;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommand;