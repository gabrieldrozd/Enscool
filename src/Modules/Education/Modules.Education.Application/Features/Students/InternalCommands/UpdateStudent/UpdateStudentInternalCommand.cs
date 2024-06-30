using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;

namespace Modules.Education.Application.Features.Students.InternalCommands.UpdateStudent;

public sealed record UpdateStudentInternalCommand(
    UserId UserId,
    Phone Phone,
    string FirstName,
    string? MiddleName,
    string LastName,
    Date BirthDate,
    Address Address
) : IInternalCommand
{
    internal sealed class UpdateStudentInternalCommandHandler : ICommandHandler<UpdateStudentInternalCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateStudentInternalCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateStudentInternalCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetAsync(request.UserId, cancellationToken);
            if (student is null) return Result.Failure.NotFound(Resource.UserNotFound, request.UserId);

            student.Update(
                request.Phone,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Address,
                request.BirthDate);

            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}