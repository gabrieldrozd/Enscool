using Common.Utilities.Primitives.Results;
using Core.Application.Auth;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Application.Abstractions.Services;

namespace Modules.Management.Application.Features.Users.Commands.Login;

internal sealed class LoginCommandHandler : ICommandHandler<LoginCommand, AccessModel>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<AccessModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetAsync(request.Email, cancellationToken);
        if (user is null || !user.CanBeLoggedIn())
            return Result.Failure.Unauthorized<AccessModel>();

        if (user.Password is not null && !user.Password.Verify(request.Password))
            return Result.Failure.Unauthorized<AccessModel>();

        var token = _tokenService.Create(user);
        return Result.Success.Ok(token);
    }
}