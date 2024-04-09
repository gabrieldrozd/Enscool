using System.Reflection;
using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Communication.Internal;

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestType = GetRequestType(typeof(TRequest));
        _logger.LogInformation("[{@Timestamp} | {@RequestType}]: Handling '{@Request}'",
            DateTime.UtcNow.ToString("s"),
            requestType,
            typeof(TRequest).Name);

        var result = await next();

        if (result.IsSuccess)
            _logger.LogInformation("[{@Timestamp} | {@RequestType}({@RequestCode})]: Completed successfully '{@Request}'",
                DateTime.UtcNow.ToString("s"),
                requestType,
                result.State.ToHttpCode(),
                typeof(TRequest).Name);

        else if (!result.IsSuccess)
            _logger.LogError("[{@Timestamp} | {@RequestType}({@RequestCode})]: Completed '{@Request}' with error '{@ResultMessage}'",
                DateTime.UtcNow.ToString("s"),
                requestType,
                result.State.ToHttpCode(),
                typeof(TRequest).Name,
                result.Message);

        return result;
    }

    private static string GetRequestType(MemberInfo type)
    {
        const string internalCommandPostfix = "InternalCommand";
        const string commandPostfix = "Command";
        const string queryPostfix = "Query";

        if (type.Name.EndsWith(internalCommandPostfix))
            return internalCommandPostfix;

        if (type.Name.EndsWith(commandPostfix))
            return commandPostfix;

        if (type.Name.EndsWith(queryPostfix))
            return queryPostfix;

        throw new InvalidOperationException($"Unknown request type {type.Name}");
    }
}