using Core.Application.Communication.Internal.Commands;
using Modules.Education.Domain.Courses;

namespace Modules.Education.Application.Features.Courses.Commands.Create;

/// <summary>
/// Creates new <see cref="Course"/>.
/// </summary>
public sealed record CreateCourseCommand(
    string Name,
    string Description,
    DateTime StartDate,
    DateTime? EndDate,
    string Level,
    Guid MainTeacherId,
    IEnumerable<Guid> StudentIds,
    // Guid CourseTypeId,
    Guid InstitutionId
) : ITransactionCommand<Guid>;