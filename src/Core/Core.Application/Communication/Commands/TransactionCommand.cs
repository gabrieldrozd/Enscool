using Common.Utilities.Primitives.Results;

namespace Core.Application.Communication.Commands;

/// <summary>
/// Marker interface for transaction command.
/// </summary>
public interface ITransactionCommand : ICommand;

/// <summary>
/// <see cref="TransactionCommand"/> - waits for all <see cref="InternalCommand"/>s to be processed before committing.
/// </summary>
/// <returns>Non-generic <see cref="Result"/>.</returns>
public abstract record TransactionCommand : ITransactionCommand;

/// <summary>
/// Marker interface for transaction command.
/// </summary>
/// <typeparam name="TResponse">Response type.</typeparam>
public interface ITransactionCommand<TResponse> : ICommand<TResponse>;

/// <summary>
/// <see cref="TransactionCommand{TResponse}"/> - waits for all <see cref="InternalCommand"/>s to be processed before committing.
/// </summary>
/// <returns>Generic <see cref="Result{T}"/>.</returns>
public abstract record TransactionCommand<TResponse> : ITransactionCommand<TResponse>;