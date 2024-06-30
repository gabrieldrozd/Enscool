using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Features.Students.InternalCommands.DeactivateStudent;

public sealed record DeactivateStudentInternalCommand(UserId UserId) : IInternalCommand
{
    internal sealed class Handler : ICommandHandler<DeactivateStudentInternalCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeactivateStudentInternalCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetAsync(request.UserId, cancellationToken);
            if (student is null) return Result.Failure.NotFound<Student>();

            student.Deactivate();

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}