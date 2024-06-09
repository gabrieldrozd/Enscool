using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.Payloads;
using Core.Domain.Shared.ValueObjects;
using Modules.Management.Application.Abstractions;
using Modules.Management.Application.Abstractions.Repositories;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.Update;

/// <summary>
/// Creates new <see cref="InstitutionUser"/> in the system.
/// </summary>
public sealed record UpdateInstitutionUserCommand : ITransactionCommand<Guid>
{
    public Guid UserId { get; init; }
    public string Phone { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string? MiddleName { get; init; }
    public string LastName { get; init; } = string.Empty;
    public DateTime? BirthDate { get; init; }
    public AddressPayload? Address { get; init; }

    internal sealed class Handler : ICommandHandler<UpdateInstitutionUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateInstitutionUserCommand request, CancellationToken cancellationToken)
        {
            var institutionUser = await _userRepository.GetInstitutionUserAsync(request.UserId, cancellationToken);
            if (institutionUser is null)
                return Result.Failure.NotFound<Guid>(Resource.UserNotFound, request.UserId);

            institutionUser.Update(
                request.Phone,
                FullName.Create(request.FirstName, request.MiddleName, request.LastName),
                request.Address?.Map(),
                Date.Create(request.BirthDate));

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok(institutionUser.Id.Value));
        }
    }
}