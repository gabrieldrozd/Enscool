using Core.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.Database.Converters;

public sealed class DateConverter : ValueConverter<Date, DateTimeOffset>
{
    public DateConverter() : base(
        date => date.Value,
        date => new Date(date)
    )
    {
    }
}