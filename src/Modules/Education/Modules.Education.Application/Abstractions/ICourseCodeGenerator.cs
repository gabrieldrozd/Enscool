using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Modules.Education.Domain.Courses;

namespace Modules.Education.Application.Abstractions;

public interface ICourseCodeGenerator
{
    Task<CourseCode> Generate(InstitutionId institutionId, LanguageLevel level, CancellationToken cancellationToken = default);
}