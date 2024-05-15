using System.Linq.Expressions;
using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Domain.Institutions;

namespace Modules.Management.Application.Features.Institutions.Queries.GetDetails;

public class GetInstitutionDetailsQueryDto : IWithExpressionMapFrom<Institution, GetInstitutionDetailsQueryDto>
{
    public Guid? InstitutionId { get; init; }
    public InstitutionState State { get; init; }
    public string? ShortName { get; init; }
    public string? FullName { get; init; }
    public string? AddressZipCode { get; init; }
    public string? AddressZipCodeCity { get; init; }
    public string? AddressCity { get; init; }
    public string? AddressHouseNumber { get; init; }
    public string? AddressState { get; init; }
    public string? AddressStreet { get; init; }

    public static Expression<Func<Institution, GetInstitutionDetailsQueryDto>> GetMapping() =>
        institution => new GetInstitutionDetailsQueryDto
        {
            InstitutionId = institution.Id,
            State = institution.State,
            ShortName = institution.ShortName,
            FullName = institution.FullName,
            AddressZipCode = institution.Address != null ? institution.Address.ZipCode : null,
            AddressZipCodeCity = institution.Address != null ? institution.Address.ZipCodeCity : null,
            AddressCity = institution.Address != null ? institution.Address.City : null,
            AddressHouseNumber = institution.Address != null ? institution.Address.HouseNumber : null,
            AddressState = institution.Address != null ? institution.Address.State : null,
            AddressStreet = institution.Address != null ? institution.Address.Street : null
        };
}