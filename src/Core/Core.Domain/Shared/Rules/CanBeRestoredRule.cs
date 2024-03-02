using Common.Utilities.Resources;
using Core.Domain.Primitives;
using Core.Domain.Primitives.Rules;

namespace Core.Domain.Shared.Rules;

public sealed class CanBeRestoredRule : IBusinessRule
{
    private readonly IEntity _entity;

    public CanBeRestoredRule(IEntity entity) => _entity = entity;

    public string Message => Resource.RestorationError;
    public bool IsInvalid() => !_entity.CanBeRestored;
}