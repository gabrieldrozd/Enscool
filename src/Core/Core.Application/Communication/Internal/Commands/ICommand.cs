using Common.Utilities.Primitives.Results;
using MediatR;

namespace Core.Application.Communication.Internal.Commands;

public interface ICommandBase;

public interface ICommand : IRequest<Result>, ICommandBase;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase;