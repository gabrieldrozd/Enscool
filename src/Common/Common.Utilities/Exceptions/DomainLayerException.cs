using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

/// <summary>
/// Represents a domain exception.
/// </summary>
public class DomainLayerException : CoreException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainLayerException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DomainLayerException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainLayerException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="args">The arguments to be formatted into the message.</param>
    public DomainLayerException(string message, params object?[] args)
        : base(string.Format(message, args))
    {
    }
}