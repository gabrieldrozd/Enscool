using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Access;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Access.Commands.ActivateAccount;

internal sealed class ActivateAccountCommandHandler : ICommandHandler<ActivateAccountCommand, AccessModel>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenManager _tokenManager;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateAccountCommandHandler(
        IUserRepository userRepository,
        ITokenManager tokenManager,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _tokenManager = tokenManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AccessModel>> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId, cancellationToken);
        if (user is null || !user.CanBeActivated())
            return Result.Failure.BadRequest<AccessModel>(Resource.UserActivationError);

        if (!user.CurrentActivationCode.Verify(request.Code))
            return Result.Failure.BadRequest<AccessModel>(Resource.UserActivationError);

        var password = Password.Create(request.Password);
        user.ActivateAccount(password);

        var accessModel = await _tokenManager.GenerateAsync(user);
        return await _unitOfWork.CommitAsync(cancellationToken)
            .Map(Result.Success.Ok(accessModel));
    }
}