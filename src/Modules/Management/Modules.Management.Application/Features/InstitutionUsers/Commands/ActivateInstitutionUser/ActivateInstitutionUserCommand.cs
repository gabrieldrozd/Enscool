using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Application.Extensions;
using Core.Domain.Shared.Enumerations.Roles;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.ActivateInstitutionUser;

/// <summary>
/// Creates xlsx file with <see cref="InstitutionUser"/>s data.
/// </summary>
public sealed record ActivateInstitutionUserCommand : ITransactionCommand
{
    public Guid UserId { get; init; }

    internal sealed class Handler : ICommandHandler<ActivateInstitutionUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ActivateInstitutionUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetInstitutionUserAsync(request.UserId, cancellationToken) is not { } institutionUser)
                return Result.Failure.NotFound<Guid>(Resource.UserNotFound, request.UserId);

            institutionUser.Reactivate();

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}