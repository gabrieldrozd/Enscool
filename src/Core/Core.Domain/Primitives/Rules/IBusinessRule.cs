namespace Core.Domain.Primitives.Rules;

/// <summary>
/// Represents the business rule.
/// </summary>
public interface IBusinessRule
{
    /// <summary>
    /// Gets the error message.
    /// </summary>
    string Message { get; }

    /// <summary>
    /// Checks if the business rule is valid.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the business rule is invalid.
    /// <c>false</c> if the business rule is valid.
    /// </returns>
    bool IsInvalid();
}