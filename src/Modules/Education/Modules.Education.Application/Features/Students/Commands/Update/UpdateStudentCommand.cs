namespace Modules.Education.Application.Features.Students.Commands.Update;

/// <summary>
/// Updates <see cref="Student"/>.
/// </summary>
// public sealed record UpdateStudentCommand : ITransactionCommand<Guid>
// {

// TODO: Modify this commented out implementation, to allow updating education-related properties rather than user-related properties
// TODO: Modify this commented out implementation, to allow updating education-related properties rather than user-related properties
// TODO: Modify this commented out implementation, to allow updating education-related properties rather than user-related properties

//     public Guid UserId { get; init; }
//     public string Phone { get; init; } = string.Empty;
//     public string FirstName { get; init; } = string.Empty;
//     public string? MiddleName { get; init; }
//     public string LastName { get; init; } = string.Empty;
//     public AddressPayload Address { get; init; } = null!;
//     public int LanguageLevel { get; init; }
//     public DateTime BirthDate { get; init; }
//
//     internal sealed class Handler : ICommandHandler<UpdateStudentCommand, Guid>
//     {
//         private readonly IStudentRepository _studentRepository;
//         private readonly IUnitOfWork _unitOfWork;
//
//         public Handler(IStudentRepository studentRepository, IUnitOfWork unitOfWork)
//         {
//             _studentRepository = studentRepository;
//             _unitOfWork = unitOfWork;
//         }
//
//         public async Task<Result<Guid>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
//         {
//             var student = await _studentRepository.GetAsync(request.UserId, cancellationToken);
//             if (student is null) return Result.Failure.NotFound<Guid>(Resource.UserNotFound, request.UserId);
//
//             student.Update(
//                 request.Phone,
//                 request.FirstName,
//                 request.MiddleName,
//                 request.LastName,
//                 request.Address.Map(),
//                 request.LanguageLevel,
//                 Date.Create(request.BirthDate));
//
//             // TODO NOTE: Implement in a similar fashion the `UpdateTeacherCommand`
//             // TODO NOTE: Implement in a similar fashion the `UpdateTeacherCommand`
//             // TODO NOTE: Implement in a similar fashion the `UpdateTeacherCommand`
//
//             return await _unitOfWork.CommitAsync(cancellationToken)
//                 .Map(Result.Success.Ok(student.Id.Value));
//         }
//     }
// }