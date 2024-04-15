namespace Common.Utilities.Extensions;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}" />.
/// </summary>
public static class EnumerableExtensions
{
    public static bool HasOther<T>(this IEnumerable<T> source, IEnumerable<T> other)
    {
        var sourceEnumerable = source as T[] ?? source.ToArray();
        var otherEnumerable = other as T[] ?? other.ToArray();
        return sourceEnumerable.Length != 0 && otherEnumerable.Length != 0 &&
            sourceEnumerable.Intersect(otherEnumerable).Count() == otherEnumerable.Length;
    }

    public static bool HasOther<T>(this IEnumerable<T> source, T other)
    {
        var sourceEnumerable = source as T[] ?? source.ToArray();
        if (other is null) return false;
        return sourceEnumerable.Length != 0 && sourceEnumerable.Contains(other);
    }

    public static List<T1> MapTo<T, T1>(
        this IEnumerable<T> source, Func<T, T1> func)
    {
        var sourceArray = source as T[] ?? source.ToArray();
        if (sourceArray.Length == 0)
        {
            return [];
        }

        var mapped = sourceArray.Select(func);
        return mapped.ToList();
    }

    public static List<T2> MapTo<T, T1, T2>(
        this IEnumerable<T> source, Func<T, T1, T2> func, T1 a)
    {
        var sourceArray = source as T[] ?? source.ToArray();
        if (sourceArray.Length == 0)
        {
            return [];
        }

        var mapped = sourceArray.Select(x => func(x, a));
        return mapped.ToList();
    }

    public static List<T> Disorder<T>(this IEnumerable<T> source)
    {
        var sourceArray = source as T[] ?? source.ToArray();
        return sourceArray.Length != 0
            ? sourceArray.OrderBy(_ => Random.Shared.Next()).ToList()
            : [];
    }

    /// <summary>
    /// Joins the elements of a sequence into a string using a separator.
    /// </summary>
    /// <param name="source">The sequence to join.</param>
    /// <param name="separator">The separator to use. Default is <c>;</c>.</param>
    /// <typeparam name="T">The type of the elements of <paramref name="source" />.</typeparam>
    /// <returns>A string that consists of the elements of <paramref name="source" /> delimited by the <paramref name="separator" /> string.</returns>
    public static string Join<T>(this IEnumerable<T> source, string separator = ";")
        => string.Join(separator, source.Select(x => x?.ToString()));
}