using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Ensures.Resources;

namespace Common.Utilities.Primitives.Ensures.EnsureNotExtensions;

public static class EnsureNotNullOrEmptyExtensions
{
    /// <summary>
    /// Checks if the value is null, empty, default or whitespace according to the type.
    /// </summary>
    /// <param name="ensureNot"/>
    /// <param name="value">T value to check.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <exception cref="ParameterException">Thrown when the value is null, empty, default or whitespace according to the type.</exception>
    public static void NullOrEmpty<T>(
        this IEnsureNot ensureNot,
        [NotNull] T? value,
        [CallerArgumentExpression("value")] string paramName = ""
    )
    {
        if (value is null)
            throw new ParameterException(EnsureResource.ParameterNull, paramName);

        if (typeof(T).IsValueType && value.Equals(default(T)))
            throw new ParameterException(EnsureResource.ParameterDefault, paramName);

        if (value is string s && string.IsNullOrWhiteSpace(s))
            throw new ParameterException(EnsureResource.ParameterNullEmptyOrWhitespace, paramName);

        if (value is IEnumerable enumerable)
            HandleEnumerable(enumerable, paramName);
    }

    private static void HandleEnumerable(IEnumerable value, string paramName)
    {
        if (value == null)
            throw new ParameterException(EnsureResource.ParameterNullOrEmpty, paramName);

        var enumerator = value.GetEnumerator();

        try
        {
            if (!enumerator.MoveNext())
                throw new ParameterException(EnsureResource.ParameterNullOrEmpty, paramName);
        }
        finally
        {
            if (enumerator is IDisposable disposable)
                disposable.Dispose();
        }
    }
}