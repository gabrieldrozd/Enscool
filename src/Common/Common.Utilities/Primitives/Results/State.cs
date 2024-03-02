namespace Common.Utilities.Primitives.Results;

public enum State
{
    /// <summary>
    /// 200 OK / 201 Created
    /// </summary>
    /// <remarks>
    /// General Description: 
    /// Successful HTTP request. The action was successfully received, understood, and accepted.
    /// <list type="bullet">
    /// <item><description>Reading a resource (GET).</description></item>
    /// <item><description>Creating a new resource (POST) that doesn't require immediate confirmation of its completion.</description></item>
    /// </list>
    /// </remarks>
    Ok,

    /// <summary>
    /// 202 Accepted
    /// </summary>
    /// <remarks>
    /// General Description: 
    /// The request has been accepted for processing, but the processing has not been completed.
    /// <list type="bullet">
    /// <item><description>Long-running processes initiated by the client.</description></item>
    /// <item><description>Asynchronous tasks like report generation.</description></item>
    /// </list>
    /// </remarks>
    Accepted,

    /// <summary>
    /// 204 No Content
    /// </summary>
    /// <remarks>
    /// General Description: 
    /// The server successfully processed the request, but is not returning any content.
    /// <list type="bullet">
    /// <item><description>DELETE operations that successfully remove a resource.</description></item>
    /// <item><description>PUT or PATCH operations that update a resource without needing to return the updated resource.</description></item>
    /// </list>
    /// </remarks>
    NoContent,

    /// <summary>
    /// 400 Bad Request
    /// </summary>
    /// <remarks>
    /// General Description: 
    /// The server cannot or will not process the request due to an apparent client error.
    /// <list type="bullet">
    /// <item><description>Validation errors.</description></item>
    /// <item><description>Missing required parameters.</description></item>
    /// </list>
    /// </remarks>
    BadRequest,

    /// <summary>
    /// 401 Unauthorized
    /// </summary>
    /// <remarks>
    /// General Description: 
    /// The request has not been applied because it lacks valid authentication credentials for the target resource.
    /// <list type="bullet">
    /// <item><description>Missing or invalid authentication token.</description></item>
    /// <item><description>Expired session.</description></item>
    /// </list>
    /// </remarks>
    Unauthorized,

    /// <summary>
    /// 403 Forbidden
    /// </summary>
    /// <remarks>
    /// General Description: 
    /// The server understands the request, but it refuses to authorize it.
    /// <list type="bullet">
    /// <item><description>User is authenticated but does not have permission for the requested operation.</description></item>
    /// <item><description>Resource is hidden or locked.</description></item>
    /// </list>
    /// </remarks>
    Forbidden,

    /// <summary>
    /// 404 Not Found
    /// </summary>
    /// <remarks>
    /// General Description: 
    /// The requested resource could not be found but may be available again in the future.
    /// <list type="bullet">
    /// <item><description>Resource does not exist.</description></item>
    /// <item><description>Invalid URL or resource ID.</description></item>
    /// </list>
    /// </remarks>
    NotFound,

    /// <summary>
    /// 500 Internal Server Error
    /// </summary>
    /// <remarks>
    /// General Description: 
    /// A generic error message, given when an unexpected condition was encountered and no more specific message is suitable.
    /// <list type="bullet">
    /// <item><description>Unhandled exceptions.</description></item>
    /// <item><description>Server misconfigurations.</description></item>
    /// </list>
    /// </remarks>
    ServerError
}