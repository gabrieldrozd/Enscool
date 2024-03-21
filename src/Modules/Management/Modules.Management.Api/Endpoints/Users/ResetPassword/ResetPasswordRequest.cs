using Common.Utilities.Models;
using Modules.Management.Application.Features.Users.Commands.ResetPassword;

namespace Modules.Management.Api.Endpoints.Users.ResetPassword;

internal sealed class ResetPasswordRequest : IWithMapTo<ResetPasswordCommand>
{
    public required string Email { get; init; }
    public required string Code { get; init; }
    public required string NewPassword { get; init; }

    public ResetPasswordCommand Map() => new(Email, Code, NewPassword);
}