using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Core.Application.Communication.External.Emails;
using Core.Application.Communication.Internal.Commands;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.ValueObjects;
using Modules.Education.Application.Abstractions;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Courses;
using Modules.Education.Domain.Students;
using Modules.Education.Domain.Teachers;

namespace Modules.Education.Application.Features.Courses.Commands.Create;

internal sealed class CreateCourseCommandHandler : ICommandHandler<CreateCourseCommand, Guid>
{
    private readonly IEmailQueue _emailQueue;
    private readonly ICourseCodeGenerator _courseCodeGenerator;
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _studentRepository;
    private readonly ITeacherRepository _teacherRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCourseCommandHandler(
        IEmailQueue emailQueue,
        ICourseCodeGenerator courseCodeGenerator,
        ICourseRepository courseRepository,
        IStudentRepository studentRepository,
        ITeacherRepository teacherRepository,
        IUnitOfWork unitOfWork)
    {
        _emailQueue = emailQueue;
        _courseCodeGenerator = courseCodeGenerator;
        _courseRepository = courseRepository;
        _studentRepository = studentRepository;
        _teacherRepository = teacherRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        // TODO: Add CourseType - unique within Institution
        // if (!await _courseTypeRepository.ExistsAsync(request.CourseTypeId))
        //     return Result.Failure.NotFound<Guid, CourseType>();

        if (!await _teacherRepository.ExistsAsync(request.MainTeacherId, cancellationToken))
            return Result.Failure.NotFound<Guid, Teacher>();

        if (!await _studentRepository.ExistAsync(UserId.From(request.StudentIds), cancellationToken))
            return Result.Failure.NotFound<Guid, Student>();

        var courseCode = await _courseCodeGenerator.Generate(request.InstitutionId, request.Level, cancellationToken);
        var course = Course.Create(
            courseCode,
            request.Level,
            request.Name,
            request.Description,
            Date.Create(request.StartDate),
            Date.Create(request.EndDate),
            request.MainTeacherId,
            request.StudentIds.Select(UserId.From),
            request.InstitutionId);

        _courseRepository.Insert(course);
        return await _unitOfWork.CommitAsync(cancellationToken)
            .Map(Result.Success.Ok(course.Id.Value));
    }
}