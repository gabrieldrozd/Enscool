using Microsoft.EntityFrameworkCore;
using Modules.Education.Domain.Courses;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Abstractions;

public interface IEducationDbContext
{
    DbSet<Course> Courses { get; }
    DbSet<Student> Students { get; }
}