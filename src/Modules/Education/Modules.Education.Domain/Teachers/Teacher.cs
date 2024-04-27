using Core.Domain.Primitives;
using Core.Domain.Shared.EntityIds;

namespace Modules.Education.Domain.Teachers;

public sealed class Teacher : AggregateRoot<UserId>
{
}