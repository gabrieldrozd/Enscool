using Common.Utilities.Resources;
using Core.Domain.Primitives.Enumerations;

namespace Core.Domain.Shared.Enumerations.Languages;

public sealed record LanguageLevel : Enumeration<LanguageLevel>
{
    public static readonly LanguageLevel None = new(0, "None");
    public static readonly LanguageLevel A0 = new(1, "A0");
    public static readonly LanguageLevel A1 = new(2, "A1");
    public static readonly LanguageLevel A2 = new(3, "A2");
    public static readonly LanguageLevel B1 = new(4, "B1");
    public static readonly LanguageLevel B2 = new(5, "B2");
    public static readonly LanguageLevel C1 = new(6, "C1");
    public static readonly LanguageLevel C2 = new(7, "C2");

    private string ResourceKey { get; }

    public string FullName => Resource.ResourceManager.GetString(ResourceKey) ?? Value;

    private LanguageLevel(int id, string value) : base(id, value)
    {
        ResourceKey = $"LanguageLevel{value}";
    }

    public static implicit operator LanguageLevel(int level) => FromId(level) ?? None;
    public static implicit operator int(LanguageLevel level) => level.Id;

    public static implicit operator LanguageLevel(string value) => FromValue(value) ?? None;
    public static implicit operator string(LanguageLevel level) => level.Value;
}