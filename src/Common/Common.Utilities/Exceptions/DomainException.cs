using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

/// <summary>
/// Represents a domain exception.
/// </summary>
public class DomainException : CoreException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DomainException(string message) : base(message)
    {
    }
}