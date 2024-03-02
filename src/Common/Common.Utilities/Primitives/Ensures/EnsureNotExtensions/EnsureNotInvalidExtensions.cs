using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Ensures.Resources;
using Common.Utilities.Resources;

namespace Common.Utilities.Primitives.Ensures.EnsureNotExtensions;

public static class EnsureNotInvalidExtensions
{
    /// <summary>
    /// Checks is the value matches the regex pattern.
    /// </summary>
    /// <param name="ensureNot"/>
    /// <param name="value">String value to check.</param>
    /// <param name="regexPattern">Regex pattern to check against.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="ParameterException">Thrown when the value does not match the regex pattern.</exception>
    public static void InvalidFormat(
        this IEnsureNot ensureNot,
        string value,
        string regexPattern,
        [CallerArgumentExpression("value")] string paramName = ""
    )
    {
        if (!Regex.IsMatch(value, regexPattern))
        {
            throw new ParameterException(EnsureResource.ParameterInvalidFormat, paramName);
        }
    }

    /// <summary>
    /// Checks if the value is a valid URI/URL.
    /// </summary>
    /// <param name="ensureNot"/>
    /// <param name="value">String value to check.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="ParameterException">Thrown when the value is not a valid URI/URL.</exception>
    public static void InvalidUri(
        this IEnsureNot ensureNot,
        string value,
        [CallerArgumentExpression("value")] string paramName = ""
    )
    {
        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
        {
            throw new ParameterException(EnsureResource.ParameterInvalidUriFormat, paramName);
        }
    }
}