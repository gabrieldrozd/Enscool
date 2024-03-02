namespace Common.Utilities.Exceptions.Base;

public abstract class CoreException : Exception
{
    protected CoreException(string message) : base(message)
    {
    }
}