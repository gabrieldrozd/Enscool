using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.Payloads;
using Modules.Management.Application.Features.InstitutionUsers.Commands.Update;

namespace Modules.Management.Api.Endpoints.InstitutionUsers.Update;

internal sealed class UpdateInstitutionUserRequest : IWithMapTo<UpdateInstitutionUserCommand>
{
    public string Phone { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = null!;
    public DateTime? BirthDate { get; init; }
    public AddressPayload? Address { get; init; }

    public UpdateInstitutionUserCommand Map() => new()
    {
        Phone = Phone,
        FirstName = FirstName,
        MiddleName = MiddleName,
        LastName = LastName,
        BirthDate = BirthDate,
        Address = Address
    };
}