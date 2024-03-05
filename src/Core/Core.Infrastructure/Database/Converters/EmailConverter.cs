using Core.Domain.Shared.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.Database.Converters;

public sealed class EmailConverter : ValueConverter<Email, string>
{
    public EmailConverter() : base(
        email => email.Value,
        email => new Email(email)
    )
    {
    }
}