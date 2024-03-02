using System.Runtime.CompilerServices;
using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Ensures.Resources;

namespace Common.Utilities.Primitives.Ensures.EnsureIsExtensions;

public static class EnsureIsValidExtensions
{
    /// <summary>
    /// Checks is the value is prefixed with the prefix.
    /// </summary>
    /// <param name="ensureIs"/>
    /// <param name="value">String value to check.</param>
    /// <param name="prefix">Prefix to check against.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="ParameterException">Thrown when the value is not prefixed with the prefix.</exception>
    public static void PrefixedWith(
        this IEnsureIs ensureIs,
        string value,
        string prefix,
        [CallerArgumentExpression("value")] string paramName = ""
    )
    {
        if (!value.StartsWith(prefix))
        {
            throw new ParameterException(EnsureResource.ParameterInvalidPrefix, paramName, prefix);
        }
    }
}