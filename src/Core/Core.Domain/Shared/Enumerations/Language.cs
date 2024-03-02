using System.Collections.Immutable;
using Core.Domain.Primitives.Enumerations;

namespace Core.Domain.Shared.Enumerations;

public sealed record Language : Enumeration<Language>
{
    public static readonly Language English = new(1, "en-US", true);
    public static readonly Language Polish = new(2, "pl-PL", true);

    public static readonly IEnumerable<Language> Active = All.Where(x => x.IsActive);
    public static readonly ImmutableArray<string> Cultures = All.Where(x => x.IsActive).Select(x => x.Value).ToImmutableArray();

    public static Language Default => English;

    public bool IsActive { get; init; }

    private Language(int id, string value, bool isActive) : base(id, value)
    {
        IsActive = isActive;
    }

    public static implicit operator Language(string value) => All.Single(x => x.Value == value);
    public static implicit operator string(Language language) => language.Value;

    public static bool IsLanguageActive(string culture)
    {
        if (string.IsNullOrEmpty(culture))
            return false;

        var firstPart = culture.Split('-')[0];
        return Active.Any(x => x.Value.Contains($"{firstPart}-"));
    }
}