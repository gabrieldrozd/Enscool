using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Features.Students.InternalCommands.ReactivateStudent;

public sealed record ReactivateStudentInternalCommand(UserId UserId) : IInternalCommand
{
    internal sealed class Handler : ICommandHandler<ReactivateStudentInternalCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ReactivateStudentInternalCommand request, CancellationToken cancellationToken)
        {
            if (await _studentRepository.GetAsync(request.UserId, cancellationToken) is not { } student)
                return Result.Failure.NotFound<Guid>(Resource.UserNotFound, request.UserId);

            student.Reactivate();

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}