using System.Transactions;
using Common.Utilities.Exceptions;
using Common.Utilities.Primitives.Results;
using Core.Application.Communication.Internal.Commands;
using MediatR;

namespace Core.Infrastructure.Communication.Internal;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class MediatorTransactionDecorator : IMediator
{
    private readonly IMediator _mediator;

    public MediatorTransactionDecorator(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
        => _mediator.CreateStream(request, cancellationToken);

    public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
        => _mediator.CreateStream(request, cancellationToken);

    public Task Publish(object notification, CancellationToken cancellationToken = default)
        => _mediator.Publish(notification, cancellationToken);

    public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
        => _mediator.Publish(notification, cancellationToken);

    public async Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest
    {
        if (IsTransactionCommand(request.GetType()))
        {
            await SendWithinTransaction(request, cancellationToken);
            return;
        }

        await _mediator.Send(request, cancellationToken);
    }

    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (IsTransactionCommand(request.GetType()))
            return (TResponse) await SendWithinTransaction(request, cancellationToken);

        var sendResult = await _mediator.Send(request, cancellationToken);
        return sendResult is Result<TResponse> { IsFailure: true } result
            ? throw new ApplicationLayerException(result.Message)
            : sendResult!;
    }

    public async Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        if (IsTransactionCommand(request.GetType()))
            return await SendWithinTransaction((IBaseRequest) request, cancellationToken);

        var sendResult = await _mediator.Send(request, cancellationToken);
        return sendResult is Result { IsFailure: true } result
            ? throw new ApplicationLayerException(result.Message)
            : sendResult;
    }

    /// <summary>
    /// Sends the specified request within a <see cref="TransactionScope"/>.
    /// </summary>
    /// <param name="request">The request to send.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The result of the request.</returns>
    private async Task<object> SendWithinTransaction(IBaseRequest request, CancellationToken cancellationToken = default)
    {
        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var result = await _mediator.Send(request, cancellationToken);

        transaction.Complete();
        return result!;
    }

    /// <summary>
    /// Determines whether the given type is <see cref="ITransactionCommand"/> or <see cref="ITransactionCommand{TResponse}"/>.
    /// </summary>
    /// <param name="requestType">The type of the request.</param>
    /// <returns><c>true</c> if the request type is transaction command; otherwise, <c>false</c>.</returns>
    private static bool IsTransactionCommand(Type? requestType)
    {
        while (requestType is not null && requestType != typeof(object))
        {
            if (requestType.IsGenericType)
            {
                var genericTypeDefinition = requestType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(ITransactionCommand<>))
                    return true;
            }
            else if (requestType == typeof(ITransactionCommand))
                return true;

            requestType = requestType.BaseType;
        }

        return false;
    }
}