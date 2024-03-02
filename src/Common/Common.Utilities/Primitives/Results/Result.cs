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
        public static Result BadRequest(string? message = null)
            => new(ResultStatus.BadRequest(message));

        public static Result<T> BadRequest<T>(string? message = null)
            => new Result(ResultStatus.BadRequest(message))
                .AsResult<T>();

        public static Result Validation(ValidationResult validationResult)
            => new(ResultStatus.BadRequest(string.Join(", ", validationResult.Errors)));

        public static Result<T> Validation<T>(ValidationResult validationResult)
            => new Result(ResultStatus.BadRequest(string.Join(", ", validationResult.Errors)))
                .AsResult<T>();

        public static Result NotFound(string? message = null)
            => new(ResultStatus.NotFound(message));

        public static Result<T> NotFound<T>(string? message = null)
            => new Result(ResultStatus.NotFound<T>(message))
                .AsResult<T>();

        public static Result<T> NotFound<T, TNotFound>(string? message = null)
            => new Result(ResultStatus.NotFound<TNotFound>(message))
                .AsResult<T>();

        public static Result Unauthorized(string? message = null)
            => new(ResultStatus.Unauthorized(message));

        public static Result<T> Unauthorized<T>(string? message = null)
            => new Result(ResultStatus.Unauthorized(message))
                .AsResult<T>();

        public static Result Forbidden(string? message = null)
            => new(ResultStatus.Forbidden(message));

        public static Result<T> Forbidden<T>(string? message = null)
            => new Result(ResultStatus.Forbidden(message))
                .AsResult<T>();
    }

    public static class Error
    {
        public static Result ServerError(string? message = null)
            => new(ResultStatus.ServerError(message));

        public static Result<T> ServerError<T>(string? message = null)
            => new Result(ResultStatus.ServerError(message))
                .AsResult<T>();
    }

    private Result<T> AsResult<T>(T? value = default) => new(Status, value);
}