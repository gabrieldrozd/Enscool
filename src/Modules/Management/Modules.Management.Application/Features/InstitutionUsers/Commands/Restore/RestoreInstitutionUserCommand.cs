using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Restore;

/// <summary>
/// Deletes <see cref="InstitutionUser"/>.
/// </summary>
public sealed record RestoreInstitutionUserCommand : ITransactionCommand
{
    public Guid UserId { get; init; }

    internal sealed class Handler : ICommandHandler<RestoreInstitutionUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RestoreInstitutionUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetDeletedInstitutionUserAsync(request.UserId, cancellationToken) is not { } institutionUser)
                return Result.Failure.NotFound<Guid>(Resource.UserNotFound, request.UserId);

            institutionUser.Restore();

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}