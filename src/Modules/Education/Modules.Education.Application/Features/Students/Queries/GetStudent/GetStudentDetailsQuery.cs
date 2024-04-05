using Core.Application.Communication.Internal.Queries;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Features.Students.Queries.GetStudent;

/// <summary>
/// Gets <see cref="Student"/> details.
/// </summary>
/// <param name="StudentId"></param>
public sealed record GetStudentDetailsQuery(Guid StudentId) : IQuery<GetStudentDetailsQueryDto>;