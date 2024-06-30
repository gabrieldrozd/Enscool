using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;

namespace Modules.Education.Application.Features.Teachers.InternalCommands.ReactivateTeacher;

public sealed record ReactivateTeacherInternalCommand(UserId UserId) : IInternalCommand
{
    internal sealed class Handler : ICommandHandler<ReactivateTeacherInternalCommand>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ReactivateTeacherInternalCommand request, CancellationToken cancellationToken)
        {
            if (await _teacherRepository.GetAsync(request.UserId, cancellationToken) is not { } teacher)
                return Result.Failure.NotFound<Guid>(Resource.UserNotFound, request.UserId);

            teacher.Reactivate();

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}