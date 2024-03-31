using Common.Utilities.Primitives.Results;

namespace Core.Application.Communication.Internal.Commands;

/// <summary>
/// Marker interface for transaction commands.
/// </summary>
public interface ITransactionCommandBase;

/// <summary>
/// <see cref="ITransactionCommand"/> - waits for all <see cref="IInternalCommand"/>s to be processed before committing.
/// </summary>
/// <returns>Non-generic <see cref="Result"/>.</returns>
public interface ITransactionCommand : ICommand, ITransactionCommandBase;

/// <summary>
/// <see cref="ITransactionCommand{TResponse}"/> - waits for all <see cref="IInternalCommand"/>s to be processed before committing.
/// </summary>
/// <returns>Generic <see cref="Result{T}"/>.</returns>
public interface ITransactionCommand<TResponse> : ICommand<TResponse>, ITransactionCommandBase;