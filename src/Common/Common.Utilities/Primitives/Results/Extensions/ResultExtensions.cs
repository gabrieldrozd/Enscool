using Common.Utilities.Exceptions;

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
            ParameterException ex => Result.Failure.BadRequest(ex.Message),
            DomainLayerException ex => Result.Failure.BadRequest(ex.Message),
            ApplicationLayerException ex => Result.Failure.BadRequest(ex.Message),
            NotAuthenticatedException ex => Result.Failure.Unauthorized(ex.Message),
            NotAllowedException ex => Result.Failure.Forbidden(ex.Message),
            NotFoundException ex => Result.Failure.NotFound(ex.Message),
            ConfigurationException ex => Result.Error.ServerError(ex.Message),
            _ => Result.Error.ServerError(exception.Message)
        };

    /// <summary>
    /// Maps the <see cref="ResultState"/> <paramref name="state" /> to an HTTP status code.
    /// </summary>
    /// <param name="state">State to map to an HTTP status code.</param>
    /// <returns>HTTP status code based on the given <paramref name="state" />.</returns>
    public static int ToHttpCode(this ResultState state)
    {
        return state switch
        {
            ResultState.Ok => 200,
            ResultState.Accepted => 202,
            ResultState.NoContent => 204,
            ResultState.BadRequest => 400,
            ResultState.Unauthorized => 401,
            ResultState.Forbidden => 403,
            ResultState.NotFound => 404,
            ResultState.ServerError => 500,
            _ => 500
        };
    }

    public static async Task<T> Match<T>(this Task<Result> resultTask, Func<T> onSuccess, Func<string, object[], T> onFailure)
    {
        var result = await resultTask;
        return result.IsFailure
            ? onFailure(result.Message!, [])
            : onSuccess();
    }

    /// <summary>
    /// Creates a <see cref="Result{TObject}" /> from the outcome of the given <paramref name="resultTask" />.
    /// </summary>
    /// <param name="resultTask">Task to create the <see cref="Result{TObject}" /> from.</param>
    /// <param name="onSuccess">Success <see cref="Result{TObject}" /> to use if the <paramref name="resultTask" /> outcome is successful.</param>
    /// <param name="onFailure">Failure <see cref="Result{TObject}" /> to use if the <paramref name="resultTask" /> outcome is not successful.</param>
    /// <typeparam name="T">Type of the outcome of <paramref name="resultTask" />.</typeparam>
    /// <returns><see cref="Result{TObject}" /> based on outcome from the given <paramref name="resultTask" />.</returns>
    public static async Task<Result<T>> Match<T>(this Task<Result> resultTask, Result<T> onSuccess, Result<T> onFailure)
    {
        var result = await resultTask;
        return result.IsFailure ? onFailure : onSuccess;
    }

    public static async Task<Result> Map(this Task<Result> resultTask, Result onSuccess)
    {
        var result = await resultTask;
        return result.IsFailure
            ? Result.Failure.BadRequest(result.Message!)
            : onSuccess;
    }

    public static async Task<Result> Map(this Task<Result> resultTask, Func<Result> onSuccess)
    {
        var result = await resultTask;
        return result.IsFailure
            ? Result.Failure.BadRequest(result.Message!)
            : onSuccess();
    }

    public static async Task<Result<T>> Map<T>(this Task<Result> resultTask, Result<T> onSuccess)
    {
        var result = await resultTask;
        return result.IsFailure
            ? Result.Failure.BadRequest<T>(result.Message!)
            : onSuccess;
    }

    public static async Task<Result<T>> Map<T>(this Task<Result> resultTask, Func<Result<T>> onSuccess)
    {
        var result = await resultTask;
        return result.IsFailure
            ? Result.Failure.BadRequest<T>(result.Message!)
            : onSuccess();
    }

    public static Result<T> OrNotFound<T>(this T? obj, string message, params object[] args)
        => obj is null
            ? Result.Failure.NotFound<T>(message, args)
            : Result.Success.Ok(obj);
}