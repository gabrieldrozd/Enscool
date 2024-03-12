using FluentValidation.Results;

namespace Common.Utilities.Primitives.Results;

/// <summary>
/// Represents the result of an operation
/// </summary>
public class Result
{
    public ResultStatus Status { get; }
    public bool IsSuccess => Status.State is State.Ok or State.Accepted or State.NoContent;
    public bool IsFailure => !IsSuccess;

    protected Result(ResultStatus status) => Status = status;

    public static class Success
    {
        public static Result Ok()
            => new(ResultStatus.Ok());

        public static Result<T> Ok<T>(T value)
            => new Result(ResultStatus.Ok())
                .AsResult(value);

        public static Result Accepted()
            => new(ResultStatus.Accepted());

        public static Result<T> Accepted<T>(T value)
            => new Result(ResultStatus.Accepted())
                .AsResult(value);

        public static Result NoContent()
            => new(ResultStatus.NoContent());

        public static Result<T> NoContent<T>(T value)
            => new Result(ResultStatus.NoContent())
                .AsResult(value);
    }

    public static class Failure
    {
        public static Result BadRequest(string? message = null, params object[] args)
            => new(ResultStatus.BadRequest(message is not null ? string.Format(message, args) : message));

        public static Result<T> BadRequest<T>(string? message = null, params object[] args)
            => new Result(ResultStatus.BadRequest(message is not null ? string.Format(message, args) : message))
                .AsResult<T>();

        public static Result Validation(ValidationResult validationResult)
            => new(ResultStatus.BadRequest(string.Join(", ", validationResult.Errors)));

        public static Result<T> Validation<T>(ValidationResult validationResult)
            => new Result(ResultStatus.BadRequest(string.Join(", ", validationResult.Errors)))
                .AsResult<T>();

        public static Result NotFound(string? message = null, params object[] args)
            => new(ResultStatus.NotFound(message is not null ? string.Format(message, args) : message));

        public static Result<T> NotFound<T>(string? message = null, params object[] args)
            => new Result(ResultStatus.NotFound<T>(message is not null ? string.Format(message, args) : message))
                .AsResult<T>();

        public static Result<T> NotFound<T, TNotFound>(string? message = null, params object[] args)
            => new Result(ResultStatus.NotFound<TNotFound>(message is not null ? string.Format(message, args) : message))
                .AsResult<T>();

        public static Result Unauthorized(string? message = null, params object[] args)
            => new(ResultStatus.Unauthorized(message is not null ? string.Format(message, args) : message));

        public static Result<T> Unauthorized<T>(string? message = null, params object[] args)
            => new Result(ResultStatus.Unauthorized(message is not null ? string.Format(message, args) : message))
                .AsResult<T>();

        public static Result Forbidden(string? message = null, params object[] args)
            => new(ResultStatus.Forbidden(message is not null ? string.Format(message, args) : message));

        public static Result<T> Forbidden<T>(string? message = null, params object[] args)
            => new Result(ResultStatus.Forbidden(message is not null ? string.Format(message, args) : message))
                .AsResult<T>();
    }

    public static class Error
    {
        public static Result ServerError(string? message = null, params object[] args)
            => new(ResultStatus.ServerError(message is not null ? string.Format(message, args) : message));

        public static Result<T> ServerError<T>(string? message = null, params object[] args)
            => new Result(ResultStatus.ServerError(message is not null ? string.Format(message, args) : message))
                .AsResult<T>();
    }

    private Result<T> AsResult<T>(T? value = default) => new(Status, value);
}