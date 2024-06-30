using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Features.Students.InternalCommands.RestoreStudent;

public sealed record RestoreStudentInternalCommand(UserId UserId) : IInternalCommand
{
    internal sealed class Handler : ICommandHandler<RestoreStudentInternalCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(RestoreStudentInternalCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetDeletedAsync(request.UserId, cancellationToken);
            if (student is null) return Result.Failure.NotFound<Student>();

            student.Restore();

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}