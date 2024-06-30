using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Core.Application.Communication.Internal.Commands;
using Core.Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Modules.Management.Application.Abstractions;
using Modules.Management.Domain.Users;

namespace Modules.Management.Application.Features.InstitutionUsers.Commands.DeactivateInstitutionUsers;

/// <summary>
/// Creates xlsx file with <see cref="InstitutionUser"/>s data.
/// </summary>
public sealed record DeactivateInstitutionUsersCommand : ITransactionCommand
{
    public List<Guid> UserIds { get; init; } = [];

    internal sealed class Handler : ICommandHandler<DeactivateInstitutionUsersCommand>
    {
        private readonly IManagementDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IManagementDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeactivateInstitutionUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _context.Users
                .AsNoTracking()
                .OfType<InstitutionUser>()
                .WhereIf(request.UserIds.Count > 0, x => request.UserIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

            if (users.Count == 0)
                return Result.Failure.NotFound("Users not found");

            users.ForEach(x => x.Deactivate());

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}