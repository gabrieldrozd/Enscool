using Common.Utilities.Primitives.Results;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions.Access;
using Modules.Management.Application.Abstractions.Repositories;

namespace Modules.Management.Application.Features.Access.Commands.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, AccessModel>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenManager _tokenManager;

    public LoginCommandHandler(IUserRepository userRepository, ITokenManager tokenManager)
    {
        _userRepository = userRepository;
        _tokenManager = tokenManager;
    }

    public async Task<Result<AccessModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Email, cancellationToken);
        if (user is null || !user.CanBeLoggedIn())
            return Result.Failure.Unauthorized<AccessModel>();

        if (user.Password is not null && !user.Password.Verify(request.Password))
            return Result.Failure.Unauthorized<AccessModel>();

        var accessModel = await _tokenManager.GenerateAsync(user);
        return Result.Success.Ok(accessModel);
    }
}