using Core.Application.Communication.Internal.Commands;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Commands.GeneratePasswordResetCode;

/// <summary>
/// Generates <see cref="PasswordResetCode"/> for the user with the specified email.
/// </summary>
public sealed record GeneratePasswordResetCodeCommand(string Email) : ITransactionCommand;