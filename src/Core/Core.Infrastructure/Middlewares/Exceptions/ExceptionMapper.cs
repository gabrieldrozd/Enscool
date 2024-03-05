using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Envelope;
using Common.Utilities.Resources;
using Core.Application.Abstractions.Exceptions;
using ApplicationException = Common.Utilities.Exceptions.ApplicationException;

namespace Core.Infrastructure.Middlewares.Exceptions;

internal sealed class ExceptionMapper : IExceptionMapper
{
    public Envelope Map(Exception exception)
        => exception switch
        {
            ParameterException ex => new Envelope(false, ex.Message).WithCode(400),
            DomainException ex => new Envelope(false, ex.Message).WithCode(400),
            ApplicationException ex => new Envelope(false, ex.Message).WithCode(400),
            NotAuthenticatedException ex => new Envelope(false, ex.Message).WithCode(401),
            NotAllowedException ex => new Envelope(false, ex.Message).WithCode(403),
            NotFoundException ex => new Envelope(false, ex.Message).WithCode(404),
            _ => new Envelope(false, Resource.ServerError).WithCode(500)
        };
}