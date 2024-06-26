using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Queries;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Delete;

/// <summary>
/// Deletes <see cref="InstitutionUser"/>.
/// </summary>
public sealed record DeleteInstitutionUserCommand : ITransactionCommand
{
    public Guid UserId { get; init; }

    internal sealed class Handler : ICommandHandler<DeleteInstitutionUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEducationQueryService _educationQueryService;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEducationQueryService educationQueryService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _educationQueryService = educationQueryService;
        }

        public async Task<Result> Handle(DeleteInstitutionUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetInstitutionUserAsync(request.UserId, cancellationToken) is not { } institutionUser)
                return Result.Failure.NotFound<Guid>(Resource.UserNotFound, request.UserId);

            institutionUser.Delete();

            if (institutionUser.Role is UserRole.Student or UserRole.Teacher && !await _educationQueryService.CanDeleteUserAsync(institutionUser.Id, institutionUser.Role, cancellationToken))
                return Result.Failure.Forbidden<Guid>(Resource.UserCannotBeDeleted, institutionUser.Id);

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}