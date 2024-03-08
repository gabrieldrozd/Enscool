using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Modules.Management.Domain.Users;

namespace Modules.Management.Infrastructure.Database.Converters;

public sealed class PasswordConverter : ValueConverter<Password, string>
{
    public PasswordConverter() : base(
        v => v.Hash,
        v => Password.Instantiate(v)
    )
    {
    }
}