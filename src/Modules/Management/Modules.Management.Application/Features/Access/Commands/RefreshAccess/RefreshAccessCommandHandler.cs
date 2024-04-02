using Common.Utilities.Primitives.Results;
using Common.Utilities.Resources;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions.Access;
using Modules.Management.Application.Abstractions.Repositories;

namespace Modules.Management.Application.Features.Access.Commands.RefreshAccess;

internal sealed class RefreshAccessCommandHandler : ICommandHandler<RefreshAccessCommand, AccessModel>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenManager _tokenManager;

    public RefreshAccessCommandHandler(IUserRepository userRepository, ITokenManager tokenManager)
    {
        _userRepository = userRepository;
        _tokenManager = tokenManager;
    }

    public async Task<Result<AccessModel>> Handle(RefreshAccessCommand request, CancellationToken cancellationToken)
    {
        if (!await _tokenManager.ValidateRefreshTokenAsync(request.UserId, request.RefreshToken))
            return Result.Failure.Unauthorized<AccessModel>();

        var user = await _userRepository.GetAsync(request.UserId, cancellationToken);
        if (user is null) return Result.Failure.NotFound<AccessModel>(Resource.UserNotFound, request.UserId);

        await _tokenManager.RevokeRefreshTokenAsync(user.Id);

        var accessModel = await _tokenManager.GenerateAsync(user);
        return Result.Success.Ok(accessModel);
    }
}