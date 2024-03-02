using Common.Utilities.Resources;
using Core.Domain.Primitives;
using Core.Domain.Primitives.Rules;

namespace Core.Domain.Shared.Rules;

public sealed class CanBeDeletedRule(IEntity entity) : IBusinessRule
{
    public string Message => Resource.DeletionError;
    public bool IsInvalid() => entity.Deleted;
}