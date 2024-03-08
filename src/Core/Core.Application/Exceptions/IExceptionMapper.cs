using Common.Utilities.Primitives.Envelope;

namespace Core.Application.Exceptions;

public interface IExceptionMapper
{
    Envelope Map(Exception exception);
}