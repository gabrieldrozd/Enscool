using Common.Utilities.Primitives.Envelope;

namespace Common.Utilities.Primitives.Results.Extensions;

public static class ResultMappings
{
    /// <summary>
    /// Maps the <see cref="Result"/> to an <see cref="EmptyEnvelope"/>.
    /// </summary>
    /// <param name="result">Result to map to an <see cref="EmptyEnvelope"/>.</param>
    /// <returns><see cref="EmptyEnvelope"/> based on the given <paramref name="result"/>.</returns>
    public static EmptyEnvelope ToEnvelope(this Result result)
    {
        var envelope = result.State switch
        {
            ResultState.Ok when result.IsSuccess => new EmptyEnvelope(),
            ResultState.Accepted when result.IsSuccess => new EmptyEnvelope(),
            ResultState.NoContent when result.IsSuccess => new EmptyEnvelope(),
            ResultState.BadRequest when !result.IsSuccess => new EmptyEnvelope(result.Message),
            ResultState.Unauthorized when !result.IsSuccess => new EmptyEnvelope(result.Message),
            ResultState.Forbidden when !result.IsSuccess => new EmptyEnvelope(result.Message),
            ResultState.NotFound when !result.IsSuccess => new EmptyEnvelope(result.Message),
            ResultState.ServerError when !result.IsSuccess => new EmptyEnvelope(result.Message),
            _ => new EmptyEnvelope(result.Message)
        };

        return envelope.WithCode(result.State.ToHttpCode());
    }

    /// <summary>
    /// Maps the <see cref="Result{T}"/> to an <see cref="Envelope{T}"/>.
    /// </summary>
    /// <param name="result">Result to map to an <see cref="Envelope{T}"/>.</param>
    /// <typeparam name="T">Type of the value in the <see cref="Result{T}"/>.</typeparam>
    /// <returns><see cref="Envelope{T}"/> based on the given <paramref name="result"/>.</returns>
    public static Envelope<T> ToEnvelope<T>(this Result<T> result)
    {
        var envelope = result.State switch
        {
            ResultState.Ok when result.IsSuccess => new Envelope<T>(result.Value!),
            ResultState.Accepted when result.IsSuccess => new Envelope<T>(result.Value!),
            ResultState.NoContent when result.IsSuccess => new Envelope<T>(result.Value!),
            ResultState.BadRequest when !result.IsSuccess => new Envelope<T>(result.Message),
            ResultState.Unauthorized when !result.IsSuccess => new Envelope<T>(result.Message),
            ResultState.Forbidden when !result.IsSuccess => new Envelope<T>(result.Message),
            ResultState.NotFound when !result.IsSuccess => new Envelope<T>(result.Message),
            ResultState.ServerError when !result.IsSuccess => new Envelope<T>(result.Message),
            _ => new Envelope<T>(result.Message)
        };

        return envelope.WithCode(result.State.ToHttpCode());
    }
}