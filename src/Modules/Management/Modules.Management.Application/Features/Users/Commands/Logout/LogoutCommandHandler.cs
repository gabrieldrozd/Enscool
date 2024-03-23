using Common.Utilities.Primitives.Results;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions.Access;
using Modules.Management.Application.Abstractions.Repositories;

namespace Modules.Management.Application.Features.Users.Commands.Logout;

internal sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
{
    private readonly IUserContext _userContext;
    private readonly IUserRepository _userRepository;
    private readonly ITokenManager _tokenManager;

    public LogoutCommandHandler(
        IUserContext userContext,
        IUserRepository userRepository,
        ITokenManager tokenManager)
    {
        _userContext = userContext;
        _userRepository = userRepository;
        _tokenManager = tokenManager;
    }

    public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _userContext.EnsureAuthenticated();
        var user = await _userRepository.GetAsync(_userContext.UserId, cancellationToken);
        if (user is null) return Result.Failure.NotFound();

        // TODO: Block access token

        await _tokenManager.RevokeRefreshTokenAsync(user.Id);
        return Result.Success.Ok();
    }
}