namespace Common.Utilities.Primitives.Results;

public interface IResult
{
    ResultStatus Status { get; }
    bool IsSuccess { get; }
    bool IsFailure { get; }
}