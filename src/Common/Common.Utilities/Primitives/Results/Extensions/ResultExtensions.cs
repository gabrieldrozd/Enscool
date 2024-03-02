using Common.Utilities.Exceptions;
using ApplicationException = Common.Utilities.Exceptions.ApplicationException;

namespace Common.Utilities.Primitives.Results.Extensions;

public static class ResultExtensions
{
    /// <summary>
    /// Maps the <paramref name="exception" /> to a <see cref="Result" />.
    /// </summary>
    /// <param name="exception">Exception to map to a <see cref="Result" />.</param>
    /// <returns><see cref="Result" /> based on the given <paramref name="exception" />.</returns>
    public static Result ToResult(this Exception exception)
        => exception switch
        {
            DomainException ex => Result.Failure.BadRequest(ex.Message),
            ApplicationException ex => Result.Failure.BadRequest(ex.Message),
            NotAuthenticatedException ex => Result.Failure.Unauthorized(ex.Message),
            NotAllowedException ex => Result.Failure.Forbidden(ex.Message),
            NotFoundException ex => Result.Failure.NotFound(ex.Message),
            _ => Result.Error.ServerError(exception.Message)
        };
}