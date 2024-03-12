using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users.Commands.ActivateAccount;

internal sealed class ActivateAccountCommandHandler : ICommandHandler<ActivateAccountCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateAccountCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(ActivateAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.UserId, cancellationToken);
        if (user is null || !user.CanBeActivated())
            return Result.Failure.BadRequest(Resource.UserActivationError);

        if (!user.CurrentActivationCode.Verify(request.Code))
            return Result.Failure.BadRequest(Resource.UserActivationError);

        var password = Password.Create(request.Password);
        user.ActivateAccount(password);

        return await _unitOfWork.CommitAsync(cancellationToken)
            .Match(Result.Success.Ok, Result.Failure.BadRequest);
    }
}