using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

public sealed class NotAllowedException : CoreException
{
    public NotAllowedException() : base("Not allowed. Lack of sufficient permissions.")
    {
    }
}