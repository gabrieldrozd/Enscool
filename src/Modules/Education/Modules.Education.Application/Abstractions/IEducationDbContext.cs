using Microsoft.EntityFrameworkCore;
using Modules.Education.Domain.Students;

namespace Modules.Education.Application.Abstractions;

public interface IEducationDbContext
{
    DbSet<Student> Students { get; }
}