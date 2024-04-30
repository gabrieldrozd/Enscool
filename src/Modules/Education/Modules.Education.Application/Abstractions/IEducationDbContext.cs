using Microsoft.EntityFrameworkCore;
using Modules.Education.Domain.Courses;
using Modules.Education.Domain.Students;
using Modules.Education.Domain.Teachers;

namespace Modules.Education.Application.Abstractions;

public interface IEducationDbContext
{
    DbSet<Course> Courses { get; }
    DbSet<Student> Students { get; }
    DbSet<Teacher> Teachers { get; }
}