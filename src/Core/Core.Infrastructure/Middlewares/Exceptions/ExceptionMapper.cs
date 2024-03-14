using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Envelope;
using Common.Utilities.Resources;
using Core.Application.Exceptions;

namespace Core.Infrastructure.Middlewares.Exceptions;

internal sealed class ExceptionMapper : IExceptionMapper
{
    public Envelope Map(Exception exception)
        => exception switch
        {
            ParameterException ex => new Envelope(false, ex.Message).WithCode(400),
            DomainLayerException ex => new Envelope(false, ex.Message).WithCode(400),
            ApplicationLayerException ex => new Envelope(false, ex.Message).WithCode(400),
            NotAuthenticatedException ex => new Envelope(false, ex.Message).WithCode(401),
            NotAllowedException ex => new Envelope(false, ex.Message).WithCode(403),
            NotFoundException ex => new Envelope(false, ex.Message).WithCode(404),
            ConfigurationException ex => new Envelope(false, ex.Message).WithCode(500),
            _ => new Envelope(false, Resource.ServerError).WithCode(500)
        };
}