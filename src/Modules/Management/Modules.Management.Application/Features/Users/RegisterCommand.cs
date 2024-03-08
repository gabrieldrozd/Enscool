using Core.Application.Communication.Commands;
using Core.Domain.Shared.Enumerations.Roles;

namespace Modules.Management.Application.Features.Users;

/// <summary>
/// Registers new <see cref="UserRole.InstitutionAdmin"/> user.
/// </summary>
public sealed record RegisterCommand(
    string Email,
    string Phone,
    string FirstName,
    string? MiddleName,
    string LastName
) : ITransactionCommand;