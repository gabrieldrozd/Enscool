using System.Net;
using Common.Utilities.Resources;

namespace Common.Utilities.Primitives.Results;

/// <summary>
/// Result status for <see cref="Result"/>.
/// Contains a <see cref="State"/>, <see cref="HttpStatusCode"/> and an optional message.
/// </summary>
public sealed class ResultStatus
{
    public State State { get; }
    public HttpStatusCode Code { get; }
    public string? Message { get; }

    private ResultStatus(State state, HttpStatusCode code, string? message = null)
    {
        State = state;
        Code = code;
        Message = message;
    }

    /// <summary>Creates a successful <see cref="ResultStatus"/> - 200 OK</summary>
    public static ResultStatus Ok() => new(State.Ok, HttpStatusCode.OK);

    /// <summary>Creates an accepted <see cref="ResultStatus"/> - 202 Accepted</summary>
    public static ResultStatus Accepted() => new(State.Accepted, HttpStatusCode.Accepted);

    /// <summary>Creates a no content <see cref="ResultStatus"/> - 204 No Content</summary>
    public static ResultStatus NoContent() => new(State.NoContent, HttpStatusCode.NoContent);

    /// <summary>Creates a bad request <see cref="ResultStatus"/> - 400 Bad Request</summary>
    public static ResultStatus BadRequest(string? message = null)
        => message is not null
            ? new ResultStatus(State.BadRequest, HttpStatusCode.BadRequest, message)
            : new ResultStatus(State.BadRequest, HttpStatusCode.BadRequest, Resource.BadRequest);

    /// <summary>Creates an unauthorized <see cref="ResultStatus"/> - 401 Unauthorized</summary>
    public static ResultStatus Unauthorized(string? message = null)
        => message is not null
            ? new ResultStatus(State.Unauthorized, HttpStatusCode.Unauthorized, message)
            : new ResultStatus(State.Unauthorized, HttpStatusCode.Unauthorized, Resource.Unauthorized);

    /// <summary>Creates a forbidden <see cref="ResultStatus"/> - 403 Forbidden</summary>
    public static ResultStatus Forbidden(string? message = null)
        => message is not null
            ? new ResultStatus(State.Forbidden, HttpStatusCode.Forbidden, message)
            : new ResultStatus(State.Forbidden, HttpStatusCode.Forbidden, Resource.Forbidden);

    /// <summary>Creates a not found <see cref="ResultStatus"/> - 404 Not Found</summary>
    public static ResultStatus NotFound(string? message = null)
        => message is not null
            ? new ResultStatus(State.NotFound, HttpStatusCode.NotFound, message)
            : new ResultStatus(State.NotFound, HttpStatusCode.NotFound, Resource.ObjectNotFound);

    /// <summary>Creates a not found <see cref="ResultStatus"/> - 404 Not Found</summary>
    public static ResultStatus NotFound<TObject>(string? message = null)
        => message is not null
            ? new ResultStatus(State.NotFound, HttpStatusCode.NotFound, message)
            : new ResultStatus(State.NotFound, HttpStatusCode.NotFound, string.Format(Resource.NotFound, typeof(TObject).Name));

    /// <summary>Creates an internal server error <see cref="ResultStatus"/> - 500 Internal Server Error</summary>
    public static ResultStatus ServerError(string? message = null)
        => message is not null
            ? new ResultStatus(State.ServerError, HttpStatusCode.InternalServerError, message)
            : new ResultStatus(State.ServerError, HttpStatusCode.InternalServerError, Resource.ServerError);
}