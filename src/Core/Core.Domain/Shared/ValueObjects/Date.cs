using Core.Domain.Primitives;

namespace Core.Domain.Shared.ValueObjects;

/// <summary>
/// Represents a date value object.
/// </summary>
public sealed class Date : ValueObject
{
    public DateTimeOffset Value { get; }
    public DateTime DateTime => Value.UtcDateTime;

    public Date(DateTimeOffset value)
    {
        Value = value;
    }

    public static Date Create(DateTimeOffset value) => new(value);
    public static Date? Create(DateTimeOffset? value) => value is not null ? new Date(value.Value) : null;

    public static Date UtcNow => new(DateTimeOffset.UtcNow);
    public static Date? Empty => null;

    public static Date FromUnixSeconds(long seconds)
        => new(DateTimeOffset.FromUnixTimeSeconds(seconds));

    public long ToUnixSeconds() => Value.ToUnixTimeSeconds();

    public Date AddSeconds(int seconds) => Value.AddSeconds(seconds);
    public Date AddMinutes(int minutes) => Value.AddMinutes(minutes);
    public Date AddHours(int hours) => Value.AddHours(hours);
    public Date AddDays(int days) => new(Value.AddDays(days));
    public Date AddMonths(int months) => new(Value.AddMonths(months));
    public Date AddYears(int years) => new(Value.AddYears(years));

    public static implicit operator DateTimeOffset(Date date) => date.Value;
    public static implicit operator Date(DateTimeOffset value) => new(value);

    public static implicit operator string(Date value) => value.ToString();
    public static implicit operator Date(string value) => new(DateTimeOffset.Parse(value));

    public static implicit operator DateTime(Date value) => value.Value.UtcDateTime;
    public static implicit operator DateTime?(Date? value) => value?.Value.UtcDateTime;

    public static bool operator <(Date first, Date second) => first.Value < second.Value;
    public static bool operator >(Date first, Date second) => first.Value > second.Value;
    public static bool operator <=(Date first, Date second) => first.Value <= second.Value;
    public static bool operator >=(Date first, Date second) => first.Value >= second.Value;

    public override string ToString() => Value.ToString("yyyy-MM-dd HH:mm:ss");
    public string ToFormattedString() => Value.ToString("dd.MM.yyyy");

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}