using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

public sealed class NotAuthenticatedException : CoreException
{
    public NotAuthenticatedException() : base("Not authenticated. Please login first or refresh token.")
    {
    }
}