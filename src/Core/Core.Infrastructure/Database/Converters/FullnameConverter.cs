using Core.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.Database.Converters;

public sealed class FullnameConverter : ValueConverter<Fullname, string>
{
    public FullnameConverter() : base(
        fullName => fullName.ToString(),
        fullName => Fullname.FromString(fullName)
    )
    {
    }
}