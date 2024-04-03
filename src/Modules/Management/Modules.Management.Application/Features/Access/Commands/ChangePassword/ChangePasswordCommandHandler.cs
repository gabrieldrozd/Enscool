using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Commands.ChangePassword;

internal sealed class ChangePasswordCommandHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordCommandHandler(
        IUserContext userContext,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userContext = userContext;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        _userContext.EnsureInstitutionUserAuthenticated();
        var user = await _userRepository.GetAsync(_userContext.UserId, cancellationToken);
        if (user is null) return Result.Failure.BadRequest(Resource.ChangePasswordError);

        if (user.Password is not null && !user.Password.Verify(request.OldPassword))
            return Result.Failure.BadRequest(Resource.ChangePasswordError);

        user.ChangePassword(Password.Create(request.NewPassword));

        return await _unitOfWork.CommitAsync(cancellationToken)
            .Map(Result.Success.Ok);
    }
}