using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

public sealed class ParameterException : CoreException
{
    public ParameterException(string message)
        : base(message)
    {
    }

    public ParameterException(string message, params object[] args)
        : base(string.Format(message, args))
    {
    }
}