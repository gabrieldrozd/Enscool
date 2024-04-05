using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Common.Utilities.Resources;
using Core.Application.Communication.Internal.Commands;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Features.Students.InternalCommands.CreateStudent;

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
            request.FullName,
            request.Address,
            request.LanguageLevel,
            request.BirthDate,
            request.InstitutionId);

        _studentRepository.Insert(student);
        return await _unitOfWork.CommitAsync(cancellationToken)
            .Map(Result.Success.Ok);
    }
}