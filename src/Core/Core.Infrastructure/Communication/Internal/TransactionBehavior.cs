using System.Diagnostics.CodeAnalysis;
using System.Transactions;
using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Results;
using Core.Application.Communication.Internal.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Communication.Internal;

public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
    where TResponse : IResult
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

    public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("[{@Timestamp} | InTransaction]: Handling '{@Request}'",
            DateTime.UtcNow.ToString("s"),
            typeof(TRequest).Name);

        return request switch
        {
            ITransactionCommandBase => await SendTransactionCommand(next),
            IInternalCommand => await SendInternalCommand(next),
            _ => HandleInvalidType()
        };
    }

    private async Task<TResponse> SendTransactionCommand(RequestHandlerDelegate<TResponse> next)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await next();

        if (!result.IsSuccess)
        {
            LogHandleFailure(result);
            throw new ApplicationLayerException(result.Status.Message);
        }

        LogHandleSuccess();
        transaction.Complete();
        return result;
    }

    private async Task<TResponse> SendInternalCommand(RequestHandlerDelegate<TResponse> next)
    {
        var result = await next();
        if (!result.IsSuccess)
        {
            LogHandleFailure(result);
            throw new ApplicationLayerException(result.Status.Message);
        }

        LogHandleSuccess();
        return result;
    }

    private void LogHandleFailure(TResponse result)
    {
        _logger.LogError("[{@Timestamp} | InTransaction]: Failed to handle '{@Request}' with: '{FailureMessage}'",
            DateTime.UtcNow.ToString("s"),
            typeof(TRequest).Name,
            result.Status.Message);
    }

    private void LogHandleSuccess()
    {
        _logger.LogInformation("[{@Timestamp} | InTransaction]: Completed '{@Request}'",
            DateTime.UtcNow.ToString("s"),
            typeof(TRequest).Name);
    }

    [DoesNotReturn]
    private TResponse HandleInvalidType()
    {
        _logger.LogError("[{@Timestamp} | InTransaction]: '{@Request}' is not a {@TransactionCommandType} or {@InternalCommandType} command",
            DateTime.UtcNow.ToString("s"),
            typeof(TRequest).Name,
            nameof(ITransactionCommandBase),
            nameof(IInternalCommand));

        throw new ApplicationLayerException($"'{typeof(TRequest).Name}' is not a {nameof(ITransactionCommandBase)} or {nameof(IInternalCommand)} command");
    }
}