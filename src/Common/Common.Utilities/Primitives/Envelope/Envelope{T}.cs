namespace Common.Utilities.Primitives.Envelope;

/// <summary>
/// Response envelope for request that returns data
/// </summary>
/// <typeparam name="T">Type of data</typeparam>
public class Envelope<T> : Envelope
{
    /// <summary>
    /// Data of type T
    /// </summary>
    public T? Data { get; set; }

    public Envelope(T data) : base(true, null)
        => Data = data;

    public Envelope(string? message) : base(false, message)
        => Data = default;

    public override Envelope<T> WithCode(int code)
    {
        StatusCode = code;
        return this;
    }
}