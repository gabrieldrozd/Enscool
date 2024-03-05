using Core.Domain.Shared.EntityIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.Database.Converters;

public sealed class UserIdConverter : ValueConverter<UserId, Guid>
{
    public UserIdConverter() : base(
        userId => userId.Value,
        userId => UserId.From(userId)
    )
    {
    }
}