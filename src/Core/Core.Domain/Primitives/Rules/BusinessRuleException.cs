using Common.Utilities.Exceptions;

namespace Core.Domain.Primitives.Rules;

public sealed class BusinessRuleException : DomainLayerException
{
    public BusinessRuleException(IBusinessRule rule) : base(rule.Message)
    {
    }
}