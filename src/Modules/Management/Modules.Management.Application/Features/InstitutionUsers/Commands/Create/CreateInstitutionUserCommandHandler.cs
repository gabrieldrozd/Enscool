using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Create;

internal sealed class CreateInstitutionUserCommandHandler : ICommandHandler<CreateInstitutionUserCommand, Guid>
{
    private readonly IInstitutionRepository _institutionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInstitutionUserCommandHandler(
        IInstitutionRepository institutionRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _institutionRepository = institutionRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateInstitutionUserCommand request, CancellationToken cancellationToken)
    {
        if (!await _institutionRepository.ExistsAsync(request.InstitutionId, cancellationToken))
            return Result.Failure.NotFound<Guid>(Resource.InstitutionNotFound, request.InstitutionId);

        if (await _userRepository.ExistsInstitutionUserAsync(request.Email, cancellationToken))
            return Result.Failure.BadRequest<Guid>(Resource.EmailTaken);

        var institutionUser = InstitutionUser.Create(
            request.Email,
            request.Phone,
            FullName.Create(request.FirstName, request.MiddleName, request.LastName),
            request.Role,
            request.Address?.Map(),
            request.LanguageLevel,
            Date.Create(request.BirthDate),
            request.InstitutionId);

        _userRepository.Insert(institutionUser);
        return await _unitOfWork.CommitAsync(cancellationToken)
            .MatchOrBadRequest(Result.Success.Ok(institutionUser.Id.Value));
    }
}