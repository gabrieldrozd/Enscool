namespace Common.Utilities.Primitives.Envelope;

/// <summary>
/// Response envelope for request that returns no data
/// </summary>
public class Envelope
{
    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Indicates if the request was successful
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// List of failures
    /// </summary>
    public string? Message { get; set; }

    protected Envelope()
    {
        IsSuccess = true;
        Message = null;
    }

    public Envelope(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public virtual Envelope WithCode(int code)
    {
        StatusCode = code;
        return this;
    }
}