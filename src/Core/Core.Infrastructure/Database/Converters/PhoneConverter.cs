using Core.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.Database.Converters;

public sealed class PhoneConverter : ValueConverter<Phone, string>
{
    public PhoneConverter() : base(
        phone => phone.Value,
        phone => new Phone(phone)
    )
    {
    }
}