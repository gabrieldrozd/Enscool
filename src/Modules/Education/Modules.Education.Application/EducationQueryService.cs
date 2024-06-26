using Core.Domain.Shared.EntityIds;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Queries;
using Microsoft.EntityFrameworkCore;
using Modules.Education.Application.Abstractions;

namespace Modules.Education.Application;

public class EducationQueryService : IEducationQueryService
{
    private readonly IEducationDbContext _context;

    public EducationQueryService(IEducationDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<bool> CanDeleteUserAsync(UserId userId, UserRole role, CancellationToken cancellationToken = default) => role switch
    {
        UserRole.Student => !await _context.Courses.AnyAsync(x => x.StudentIds.Contains(userId), cancellationToken),
        UserRole.Teacher => !await _context.Courses.AnyAsync(x => x.MainTeacherId == userId || x.TeacherIds.Contains(userId), cancellationToken),
        _ => true
    };
}