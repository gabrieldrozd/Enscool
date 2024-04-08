using FluentValidation.Results;

namespace Common.Utilities.Primitives.Results;

/// <summary>
/// Represents the result of an operation
/// </summary>
public class Result : IResult
{
    public ResultState State { get; }
    public bool IsSuccess => State is ResultState.Ok or ResultState.Accepted or ResultState.NoContent;
    public bool IsFailure => !IsSuccess;
    public string? Message { get; }

    protected Result(ResultState state, string? message = null)
    {
        State = state;
        Message = message;
    }

    public static class Success
    {
        public static Result Ok()
            => new(ResultState.Ok);

        public static Result<T> Ok<T>(T value)
            => new Result(ResultState.Ok)
                .AsResult(value);

        public static Result Accepted()
            => new(ResultState.Accepted);

        public static Result<T> Accepted<T>(T value)
            => new Result(ResultState.Accepted)
                .AsResult(value);

        public static Result NoContent()
            => new(ResultState.NoContent);

        public static Result<T> NoContent<T>(T value)
            => new Result(ResultState.NoContent)
                .AsResult(value);
    }

    // TODO: Adjust messages as in ResultStatus
    // TODO: Then remove ResultStatus completely and fix errors

    public static class Failure
    {
        public static Result BadRequest(string? message = null, params object[] args)
            => new(ResultState.BadRequest, message is not null ? string.Format(message, args) : message);

        public static Result<T> BadRequest<T>(string? message = null, params object[] args)
            => new Result(ResultState.BadRequest, message is not null ? string.Format(message, args) : message)
                .AsResult<T>();

        public static Result Validation(ValidationResult validationResult)
            => new(ResultState.BadRequest, string.Join(", ", validationResult.Errors));

        public static Result<T> Validation<T>(ValidationResult validationResult)
            => new Result(ResultState.BadRequest, string.Join(", ", validationResult.Errors))
                .AsResult<T>();

        public static Result NotFound(string? message = null, params object[] args)
            => new(ResultState.NotFound, message is not null ? string.Format(message, args) : message);

        public static Result<T> NotFound<T>(string? message = null, params object[] args)
            => new Result(ResultState.NotFound, message is not null ? string.Format(message, args) : message)
                .AsResult<T>();

        public static Result<T> NotFound<T, TNotFound>(string? message = null, params object[] args)
            => new Result(ResultState.NotFound, message is not null ? string.Format(message, args) : message)
                .AsResult<T>();

        public static Result Unauthorized(string? message = null, params object[] args)
            => new(ResultState.Unauthorized, message is not null ? string.Format(message, args) : message);

        public static Result<T> Unauthorized<T>(string? message = null, params object[] args)
            => new Result(ResultState.Unauthorized, message is not null ? string.Format(message, args) : message)
                .AsResult<T>();

        public static Result Forbidden(string? message = null, params object[] args)
            => new(ResultState.Forbidden, message is not null ? string.Format(message, args) : message);

        public static Result<T> Forbidden<T>(string? message = null, params object[] args)
            => new Result(ResultState.Forbidden, message is not null ? string.Format(message, args) : message)
                .AsResult<T>();
    }

    public static class Error
    {
        public static Result ServerError(string? message = null, params object[] args)
            => new(ResultState.ServerError, message is not null ? string.Format(message, args) : message);

        public static Result<T> ServerError<T>(string? message = null, params object[] args)
            => new Result(ResultState.ServerError, message is not null ? string.Format(message, args) : message)
                .AsResult<T>();
    }

    private Result<T> AsResult<T>(T? value = default) => new(State, value);
}