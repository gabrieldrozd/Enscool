using Common.Utilities.Exceptions.Base;

namespace Common.Utilities.Exceptions;

public sealed class NotFoundException : CoreException
{
    public NotFoundException(string message)
        : base(message)
    {
    }

    public NotFoundException(string objectName, Guid objectId)
        : base(objectId.Equals(Guid.Empty)
            ? $"{objectName} was not found."
            : $"{objectName} with: '{objectId:D}' was not found.")
    {
    }
}