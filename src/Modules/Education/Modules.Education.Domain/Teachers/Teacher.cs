using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;

namespace Modules.Education.Domain.Teachers;

public sealed class Teacher : AggregateRoot<UserId>
{
    private Teacher()
    {
    }

    private Teacher(UserId teacherId)
        : base(teacherId)
    {
    }

    public static Teacher Create(UserId teacherId) => new(teacherId);
}