using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

/// <summary>
/// Represents an application configuration exception.
/// </summary>
public sealed class ConfigurationException : CoreException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConfigurationException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ConfigurationException(string? message = null) : base(message ?? "An error occured while configuring the application.")
    {
    }
}