using System.Globalization;
using Core.Domain.Primitives;
using Core.Domain.Shared.Enumerations;

namespace Core.Infrastructure.Middlewares.Culture;

public sealed class UserCulture : ValueObject
{
    public static readonly CultureInfo DefaultCulture = CultureInfo.GetCultureInfo(Language.Default);

    public CultureInfo Value { get; }

    private UserCulture(string culture)
        => Value = Language.IsLanguageActive(culture)
            ? CultureInfo.GetCultureInfo(culture)
            : DefaultCulture;

    public static UserCulture Create(string culture)
        => new(culture);

    public override string ToString() => Value.Name;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}