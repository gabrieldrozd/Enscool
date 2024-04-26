using Core.Infrastructure.Database.Repositories;
using Modules.Education.Application.Abstractions.Repositories;
using Modules.Education.Domain.Courses;

namespace Modules.Education.Infrastructure.Database.Repositories;

internal sealed class CourseRepository : Repository<Course, EducationDbContext>, ICourseRepository
{
    private readonly EducationDbContext _context;

    public CourseRepository(EducationDbContext context) : base(context)
    {
        _context = context;
    }
}