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

    public EmptyEnvelope(bool isSuccess, string? message)
        : base(isSuccess, message)
    {
    }

    public override EmptyEnvelope WithCode(int code)
    {
        StatusCode = code;
        return this;
    }
}