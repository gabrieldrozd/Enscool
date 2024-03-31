using Core.Domain.Shared.Enumerations.Languages;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.Database.Converters;

public sealed class LanguageLevelConverter : ValueConverter<LanguageLevel, int>
{
    public LanguageLevelConverter() : base(
        languageLevel => languageLevel.Id,
        languageLevel => LanguageLevel.From(languageLevel)
    )
    {
    }
}