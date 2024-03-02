namespace Common.Utilities.Primitives.Results;

/// <summary>
/// Generic result of an operation
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class Result<T> : Result
{
    private readonly T? _value;

    /// <summary>
    /// Value of type T.
    /// </summary>
    public T? Value => IsSuccess ? _value : default;

    internal Result(ResultStatus status, T? value) : base(status)
        => _value = value;

    public static implicit operator Result<T>(T value) => Success.Ok(value);
}