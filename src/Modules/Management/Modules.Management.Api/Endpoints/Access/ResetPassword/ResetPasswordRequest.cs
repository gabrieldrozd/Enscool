using Common.Utilities.Abstractions.Mapping;
using Modules.Management.Application.Features.Access.Commands.ResetPassword;

namespace Modules.Management.Api.Endpoints.Access.ResetPassword;

internal sealed class ResetPasswordRequest : IWithMapTo<ResetPasswordCommand>
{
    public required string Email { get; init; }
    public required string Code { get; init; }
    public required string NewPassword { get; init; }

    public ResetPasswordCommand Map() => new(Email, Code, NewPassword);
}