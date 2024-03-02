using Common.Utilities.Primitives.Envelope;

namespace Core.Application.Abstractions.Exceptions;

public interface IExceptionMapper
{
    Envelope Map(Exception exception);
}