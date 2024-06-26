using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Core.Domain.Shared.Enumerations.UserStates;
using Core.Domain.Shared.ValueObjects;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Features.Students.InternalCommands.CreateStudent;

public sealed record CreateStudentInternalCommand(
    UserId UserId,
    UserState State,
    Email Email,
    Phone Phone,
    string FirstName,
    string? MiddleName,
    string LastName,
    Date BirthDate,
    Address Address,
    LanguageLevel LanguageLevel,
    InstitutionId InstitutionId
) : IInternalCommand
{
    internal sealed class CreateStudentInternalCommandHandler : ICommandHandler<CreateStudentInternalCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateStudentInternalCommandHandler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateStudentInternalCommand request, CancellationToken cancellationToken)
        {
            if (await _studentRepository.ExistsWithinInstitutionAsync(request.Email, request.InstitutionId, cancellationToken))
                return Result.Failure.BadRequest(Resource.EmailTaken);

            var student = Student.Create(
                request.UserId,
                request.State,
                request.Email,
                request.Phone,
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.Address,
                request.LanguageLevel,
                request.BirthDate,
                request.InstitutionId);

            _studentRepository.Insert(student);
            return await _unitOfWork.CommitAsync(cancellationToken)
                .Map(Result.Success.Ok);
        }
    }
}