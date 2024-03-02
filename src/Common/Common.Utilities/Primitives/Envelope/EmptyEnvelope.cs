namespace Common.Utilities.Primitives.Envelope;

/// <summary>
/// Response envelope for request that returns no data
/// </summary>
public sealed class EmptyEnvelope : Envelope<EmptyData>
{
    public EmptyEnvelope()
        : base(new EmptyData())
    {
    }

    public EmptyEnvelope(string? message)
        : base(message)
    {
    }

    public override EmptyEnvelope WithCode(int code)
    {
        StatusCode = code;
        return this;
    }
}