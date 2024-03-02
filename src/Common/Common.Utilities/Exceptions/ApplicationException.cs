using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

/// <summary>
/// Represents an application operation exception.
/// </summary>
public sealed class ApplicationException : CoreException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ApplicationException(string? message = null) : base(message ?? "An error occurred during application operation.")
    {
    }
}