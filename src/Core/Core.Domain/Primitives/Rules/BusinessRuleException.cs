using Common.Utilities.Exceptions;

namespace Core.Domain.Primitives.Rules;

public sealed class BusinessRuleException : DomainException
{
    public BusinessRuleException(IBusinessRule rule) : base(rule.Message)
    {
    }
}