namespace Common.Utilities.Primitives.Results;

public interface IResult
{
    ResultState State { get; }
    bool IsSuccess { get; }
    bool IsFailure { get; }
    string? Message { get; }
}