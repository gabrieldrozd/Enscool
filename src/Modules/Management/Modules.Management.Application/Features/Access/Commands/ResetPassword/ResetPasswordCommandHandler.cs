using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Commands.ResetPassword;

internal sealed class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ResetPasswordCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Email, cancellationToken);
        if (user is null) return Result.Failure.BadRequest(Resource.PasswordResetError);

        if (!user.CanResetPassword() || !user.CurrentPasswordResetCode.Verify(request.Code))
            return Result.Failure.BadRequest(Resource.PasswordResetError);

        user.ResetPassword(Password.Create(request.NewPassword));

        return await _unitOfWork.CommitAsync(cancellationToken)
            .MatchOrBadRequest(Result.Success.Ok);
    }
}