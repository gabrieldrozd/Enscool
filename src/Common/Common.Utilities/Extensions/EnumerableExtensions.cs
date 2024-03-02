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
        if (sourceArray.Length == 0)
        {
            return [];
        }

        var random = new Random();
        var result = sourceArray.OrderBy(_ => random.Next());
        return result.ToList();
    }
}