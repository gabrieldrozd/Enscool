using System.Text;
using Common.Utilities.Extensions;
using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Languages;
using Microsoft.EntityFrameworkCore;
using Modules.Education.Application.Abstractions;
using Modules.Education.Domain.Courses;
using Modules.Education.Infrastructure.Database;

namespace Modules.Education.Infrastructure.Services;

internal sealed class CourseCodeGenerator : ICourseCodeGenerator
{
    private readonly EducationDbContext _context;

    public CourseCodeGenerator(EducationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseCode> Generate(InstitutionId institutionId, LanguageLevel level, CancellationToken cancellationToken = default)
    {
        var codeBuilder = new StringBuilder();
        codeBuilder.Append(level.Value);

        var lastCode = await _context.Courses
            .Where(x => x.Code.Value.StartsWith(level.Value))
            .OrderByDescending(x => x.Code.Value)
            .Select(x => x.Code.Value)
            .FirstOrDefaultAsync(cancellationToken);

        codeBuilder.Append(Functional.IfElse(
            condition: lastCode is not null,
            ifTrue: () =>
            {
                var lastCodeNumber = int.Parse(lastCode![level.Value.Length..]);
                return (lastCodeNumber + 1).ToString("D4");
            },
            ifFalse: () => "0001"));

        return CourseCode.From(codeBuilder.ToString());
    }
}