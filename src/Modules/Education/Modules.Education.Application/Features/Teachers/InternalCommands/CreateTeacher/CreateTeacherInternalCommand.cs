using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Teachers;

namespace Modules.Education.Application.Features.Teachers.InternalCommands.CreateTeacher;

public sealed record CreateTeacherInternalCommand(
    UserId UserId,
    UserState State,
    Email Email,
    Phone Phone,
    string FirstName,
    string? MiddleName,
    string LastName,
    Address Address,
    InstitutionId InstitutionId
) : IInternalCommand
{
    internal sealed class CreateTeacherInternalCommandHandler : ICommandHandler<CreateTeacherInternalCommand>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateTeacherInternalCommandHandler(ITeacherRepository teacherRepository, IUnitOfWork unitOfWork)
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateTeacherInternalCommand request, CancellationToken cancellationToken)
        {
            if (await _teacherRepository.ExistsWithinInstitutionAsync(request.Email, request.InstitutionId, cancellationToken))
                return Result.Failure.BadRequest(Resource.EmailTaken);

            var student = Teacher.Create(
                request.UserId,
                request.State,
                request.Email,
                request.Phone,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Address,
                request.InstitutionId);

            _teacherRepository.Insert(student);
            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}