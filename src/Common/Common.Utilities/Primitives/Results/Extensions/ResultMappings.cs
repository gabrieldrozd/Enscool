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
        var envelope = result.Status.State switch
        {
            State.Ok when result.IsSuccess => new EmptyEnvelope(),
            State.Accepted when result.IsSuccess => new EmptyEnvelope(),
            State.NoContent when result.IsSuccess => new EmptyEnvelope(),
            State.BadRequest when !result.IsSuccess => new EmptyEnvelope(result.Status.Message),
            State.Unauthorized when !result.IsSuccess => new EmptyEnvelope(result.Status.Message),
            State.Forbidden when !result.IsSuccess => new EmptyEnvelope(result.Status.Message),
            State.NotFound when !result.IsSuccess => new EmptyEnvelope(result.Status.Message),
            State.ServerError when !result.IsSuccess => new EmptyEnvelope(result.Status.Message),
            _ => new EmptyEnvelope(result.Status.Message)
        };

        return envelope.WithCode((int) result.Status.Code);
    }

    /// <summary>
    /// Maps the <see cref="Result{T}"/> to an <see cref="Envelope{T}"/>.
    /// </summary>
    /// <param name="result">Result to map to an <see cref="Envelope{T}"/>.</param>
    /// <typeparam name="T">Type of the value in the <see cref="Result{T}"/>.</typeparam>
    /// <returns><see cref="Envelope{T}"/> based on the given <paramref name="result"/>.</returns>
    public static Envelope<T> ToEnvelope<T>(this Result<T> result)
    {
        var envelope = result.Status.State switch
        {
            State.Ok when result.IsSuccess => new Envelope<T>(result.Value!),
            State.Accepted when result.IsSuccess => new Envelope<T>(result.Value!),
            State.NoContent when result.IsSuccess => new Envelope<T>(result.Value!),
            State.BadRequest when !result.IsSuccess => new Envelope<T>(result.Status.Message),
            State.Unauthorized when !result.IsSuccess => new Envelope<T>(result.Status.Message),
            State.Forbidden when !result.IsSuccess => new Envelope<T>(result.Status.Message),
            State.NotFound when !result.IsSuccess => new Envelope<T>(result.Status.Message),
            State.ServerError when !result.IsSuccess => new Envelope<T>(result.Status.Message),
            _ => new Envelope<T>(result.Status.Message)
        };

        return envelope.WithCode((int) result.Status.Code);
    }
}