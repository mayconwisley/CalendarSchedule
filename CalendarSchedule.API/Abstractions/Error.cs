namespace CalendarSchedule.API.Abstractions;

public record Error(string Code, string Message)
{
    public static Error None(string message) => new(string.Empty, message);
    public static Error NullValue(string message) => new("NullValue", message);
    public static Error NotFound(string message) => new("NotFound", message);
    public static Error Invalid(string message) => new("Invalid", message);
    public static Error AlreadyExists(string message) => new("AlreadyExists", message);
    public static Error Unauthorized(string message) => new("Unauthorized", message);
    public static Error Forbidden(string message) => new("Forbidden", message);
    public static Error Internal(string message) => new("Internal", message);
    public static Error BadRequest(string message) => new("BadRequest", message);
    public static Error Validation(string message) => new("Validation", message);
    public static Error Conflict(string message) => new("Conflict", message);
    public static Error NotImplemented(string message) => new("NotImplemented", message);
    public static Error ServiceUnavailable(string message) => new("ServiceUnavailable", message);
    public static Error UnprocessableEntity(string message) => new("UnprocessableEntity", message);
    public static Error TooManyRequests(string message) => new("TooManyRequests", message);
    public static Error GatewayTimeout(string message) => new("GatewayTimeout", message);
    public static Error NotAcceptable(string message) => new("NotAcceptable", message);
    public static Error UnsupportedMediaType(string message) => new("UnsupportedMediaType", message);
    public static Error PreconditionFailed(string message) => new("PreconditionFailed", message);
    public static Error MethodNotAllowed(string message) => new("MethodNotAllowed", message);
    public static Error RequestTimeout(string message) => new("RequestTimeout", message);
    public static Error Unexpected(string message) => new("Unexpected", message);
}

