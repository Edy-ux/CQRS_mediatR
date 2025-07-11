namespace CQRS_mediatR.Application.Errors;

public enum TypeError
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    ServerError
}

public record BusinessError(string message, TypeError errorType);

public record CreationPlayerError(string message, TypeError errorType) : BusinessError(message, errorType)
{
    public CreationPlayerError(TypeError ErrorType) : this("", ErrorType)
    {
    }
}
