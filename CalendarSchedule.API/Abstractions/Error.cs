using System.Net;

namespace CalendarSchedule.API.Abstractions;

public record Error(string Code, string Message, HttpStatusCode StatusCode)
{
    public static Error None(string message) => new(string.Empty, message, HttpStatusCode.OK);
    public static Error NullValue(string message) => new("NullValue", message, HttpStatusCode.BadRequest);
    public static Error NotFound(string message) => new("NotFound", message, HttpStatusCode.NotFound);
    public static Error Invalid(string message) => new("Invalid", message, HttpStatusCode.BadRequest);
    public static Error AlreadyExists(string message) => new("AlreadyExists", message, HttpStatusCode.Conflict);
    public static Error Unauthorized(string message) => new("Unauthorized", message, HttpStatusCode.Unauthorized);
    public static Error Forbidden(string message) => new("Forbidden", message, HttpStatusCode.Forbidden);
    public static Error Internal(string message) => new("Internal", message, HttpStatusCode.InternalServerError);
    public static Error BadRequest(string message) => new("BadRequest", message, HttpStatusCode.BadRequest);
    public static Error Validation(string message) => new("Validation", message, HttpStatusCode.UnprocessableEntity);
    public static Error Conflict(string message) => new("Conflict", message, HttpStatusCode.Conflict);
    public static Error NotImplemented(string message) => new("NotImplemented", message, HttpStatusCode.NotImplemented);
    public static Error ServiceUnavailable(string message) => new("ServiceUnavailable", message, HttpStatusCode.ServiceUnavailable);
    public static Error UnprocessableEntity(string message) => new("UnprocessableEntity", message, HttpStatusCode.UnprocessableEntity);
    public static Error TooManyRequests(string message) => new("TooManyRequests", message, (HttpStatusCode)429);
    public static Error GatewayTimeout(string message) => new("GatewayTimeout", message, HttpStatusCode.GatewayTimeout);
    public static Error NotAcceptable(string message) => new("NotAcceptable", message, HttpStatusCode.NotAcceptable);
    public static Error UnsupportedMediaType(string message) => new("UnsupportedMediaType", message, HttpStatusCode.UnsupportedMediaType);
    public static Error PreconditionFailed(string message) => new("PreconditionFailed", message, HttpStatusCode.PreconditionFailed);
    public static Error MethodNotAllowed(string message) => new("MethodNotAllowed", message, HttpStatusCode.MethodNotAllowed);
    public static Error RequestTimeout(string message) => new("RequestTimeout", message, HttpStatusCode.RequestTimeout);
    public static Error Unexpected(string message) => new("Unexpected", message, HttpStatusCode.InternalServerError);
}

