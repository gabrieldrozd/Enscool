using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

public sealed class NotAuthenticatedException : CoreException
{
    public NotAuthenticatedException() : base("Not authenticated. You need to be authenticated to perform this action.")
    {
    }

    public NotAuthenticatedException(string message) : base($"Not authenticated. {message}.")
    {
    }
}