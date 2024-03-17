using Common.Utilities.Primitives.Results;

namespace Core.Application.Communication.Internal.Commands;

/// <summary>
/// <see cref="IInternalCommand"/> - committed within the parent <see cref="TransactionCommand{TResponse}"/>.
/// </summary>
/// <returns>Non-generic <see cref="Result"/>.</returns>
public interface IInternalCommand : ICommand;