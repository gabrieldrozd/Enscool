using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Teachers;

namespace Modules.Education.Application.Features.Teachers.InternalCommands.DeleteTeacher;

public sealed record DeleteTeacherInternalCommand(UserId UserId) : IInternalCommand
{
    internal sealed class CreateTeacherInternalCommandHandler : ICommandHandler<DeleteTeacherInternalCommand>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeacherInternalCommandHandler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteTeacherInternalCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetAsync(request.UserId, cancellationToken);
            if (teacher is null) return Result.Failure.NotFound<Teacher>();

            teacher.Delete();

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}