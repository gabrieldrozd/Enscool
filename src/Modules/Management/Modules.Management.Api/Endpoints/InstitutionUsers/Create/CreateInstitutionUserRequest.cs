using Common.Utilities.Abstractions.Mapping;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Payloads;
using Modules.Management.Application.Features.InstitutionUsers.Commands.Create;

namespace Modules.Management.Api.Endpoints.InstitutionUsers.Create;

internal sealed class CreateInstitutionUserRequest : IWithMapTo<CreateInstitutionUserCommand>
{
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = null!;
    public DateTime? BirthDate { get; init; }
    public AddressPayload? Address { get; init; }
    public int? LanguageLevel { get; init; }
    public InstitutionUserRole Role { get; init; }
    public Guid InstitutionId { get; init; }

    public CreateInstitutionUserCommand Map() => new(Email, Phone, FirstName, MiddleName, LastName, BirthDate, Address, LanguageLevel, Role, InstitutionId);
}