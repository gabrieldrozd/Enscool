using Common.Utilities.Primitives.Results;
using Common.Utilities.Resources;
using Core.Application.Auth;
using Core.Application.Communication.Commands;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.Users;

internal sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.DoesInstitutionUserExistAsync(request.Email, cancellationToken))
            return Result.Failure.BadRequest<AccessToken>(Resource.EmailTaken);

        var user = User.CreateInitialInstitutionAdmin(
            Email.Parse(request.Email),
            Phone.Parse(request.Phone),
            FullName.Create(request.FirstName, request.MiddleName, request.LastName));

        _userRepository.Insert(user);
        var result = await _unitOfWork.CommitAsync(cancellationToken);
        // TODO: Send email verification here (with activation link)
        return result;
    }
}