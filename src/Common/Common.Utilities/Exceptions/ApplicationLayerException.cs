using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

/// <summary>
/// Represents an application operation exception.
/// </summary>
public sealed class ApplicationLayerException : CoreException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationLayerException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ApplicationLayerException(string? message = null) : base(message ?? "An error occurred during application operation.")
    {
    }
}