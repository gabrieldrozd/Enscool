using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;

namespace Modules.Education.Application.Features.Teachers.InternalCommands.UpdateTeacher;

public sealed record UpdateTeacherInternalCommand(
    UserId UserId,
    Phone Phone,
    string FirstName,
    string? MiddleName,
    string LastName,
    Address Address
) : IInternalCommand
{
    internal sealed class Handler : ICommandHandler<UpdateTeacherInternalCommand>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateTeacherInternalCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetAsync(request.UserId, cancellationToken);
            if (teacher is null) return Result.Failure.NotFound(Resource.UserNotFound, request.UserId);

            teacher.Update(
                request.Phone,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Address);

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}