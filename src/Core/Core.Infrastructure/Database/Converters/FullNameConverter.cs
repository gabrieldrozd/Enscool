using Core.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.Database.Converters;

public sealed class FullNameConverter : ValueConverter<FullName, string>
{
    public FullNameConverter() : base(
        fullName => fullName.ToString(),
        fullName => FullName.FromString(fullName)
    )
    {
    }
}