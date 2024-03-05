using Core.Domain.Shared.EntityIds;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Core.Infrastructure.Database.Converters;

public sealed class InstitutionIdConverter : ValueConverter<InstitutionId, Guid>
{
    public InstitutionIdConverter() : base(
        institutionId => institutionId.Value,
        institutionId => InstitutionId.From(institutionId)
    )
    {
    }
}