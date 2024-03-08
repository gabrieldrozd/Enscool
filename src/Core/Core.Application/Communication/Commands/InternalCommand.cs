using Common.Utilities.Primitives.Results;

namespace Core.Application.Communication.Commands;

/// <summary>
/// Marker interface for internal command.
/// </summary>
public interface IInternalCommand : ICommand;

/// <summary>
/// <see cref="InternalCommand"/> - committed within the parent <see cref="TransactionCommand{TResponse}"/>.
/// </summary>
/// <returns>Non-generic <see cref="Result"/>.</returns>
public abstract record InternalCommand : IInternalCommand;