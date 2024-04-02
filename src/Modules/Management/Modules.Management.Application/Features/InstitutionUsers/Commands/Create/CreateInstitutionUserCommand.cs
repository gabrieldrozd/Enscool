using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Domain.Shared.Payloads;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Create;

/// <summary>
/// Creates new <see cref="InstitutionUser"/> in the system.
/// </summary>
public sealed record CreateInstitutionUserCommand(
    string Email,
    string Phone,
    string FirstName,
    string? MiddleName,
    string LastName,
    DateTime? BirthDate,
    AddressPayload? Address,
    int? LanguageLevel,
    InstitutionUserRole Role,
    Guid InstitutionId
) : ITransactionCommand<Guid>;