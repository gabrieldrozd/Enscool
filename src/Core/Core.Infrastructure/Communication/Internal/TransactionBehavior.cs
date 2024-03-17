using System.Transactions;
using Core.Application.Communication.Internal.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Communication.Internal;

public sealed class NonGenericTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionCommand
{
    private readonly ILogger<NonGenericTransactionBehavior<TRequest, TResponse>> _logger;

    public NonGenericTransactionBehavior(ILogger<NonGenericTransactionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("[{@Timestamp} | Transaction]: Handling '{@Request}'",
            DateTime.UtcNow.ToString("s"),
            typeof(TRequest).Name);

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await next();
        transaction.Complete();

        _logger.LogInformation("[{@Timestamp} | Transaction]: Completed '{@Request}'",
            DateTime.UtcNow.ToString("s"),
            typeof(TRequest).Name);

        return result;
    }
}

public sealed class GenericTransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionCommand<TResponse>
{
    private readonly ILogger<GenericTransactionBehavior<TRequest, TResponse>> _logger;

    public GenericTransactionBehavior(ILogger<GenericTransactionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("[{@Timestamp} | Transaction]: Handling '{@Request}'",
            DateTime.UtcNow.ToString("s"),
            typeof(TRequest).Name);

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        var result = await next();
        transaction.Complete();

        _logger.LogInformation("[{@Timestamp} | Transaction]: Completed '{@Request}'",
            DateTime.UtcNow.ToString("s"),
            typeof(TRequest).Name);

        return result;
    }
}