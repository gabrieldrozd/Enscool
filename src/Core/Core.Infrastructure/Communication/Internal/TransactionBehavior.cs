using System.Transactions;
using Core.Application.Communication.Internal.Commands;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Communication.Internal;

public sealed class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionCommand
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