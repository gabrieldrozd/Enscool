using Common.Utilities.Primitives.Results;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Core.Infrastructure.Communication.Internal;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = await Task.WhenAll(_validators
            .Select(v => v.ValidateAsync(context, cancellationToken)));

        var failuresList = failures
            .Where(vr => !vr.IsValid)
            .ToList();

        if (failuresList.Count != 0)
            return CreateResult<TResponse>(failuresList);

        return await next();
    }

    private static TResult CreateResult<TResult>(IEnumerable<ValidationResult> failures)
        where TResult : Result
    {
        var errors = failures.SelectMany(f => f.Errors).ToList();
        var validation = new ValidationResult(errors);

        return typeof(TResult) == typeof(Result)
            ? (Result.Failure.Validation(validation) as TResult)!
            : (TResult) typeof(Result.Failure)
                .GetMethods()
                .Single(m => m is { Name: nameof(Result.Failure.Validation), IsGenericMethodDefinition: true })
                .MakeGenericMethod(typeof(TResult).GenericTypeArguments[0])
                .Invoke(null, [validation])!;
    }
}