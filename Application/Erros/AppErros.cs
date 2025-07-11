namespace CQRS_mediatR.Application.Errors;

public enum TypeError
{
    Validation,
    NotFound,
    Conflict,
    Unauthorized,
    ServerError,
    Technical
}

public record BusinessError(string message, TypeError errorType);

public record CreationPlayerError(string message, TypeError errorType) : BusinessError(message, errorType)
{
    public CreationPlayerError(TypeError ErrorType) : this("", ErrorType)
    {
    }
}
public record GamePlayerNotFound(string message, TypeError error) : BusinessError(message, error)
{
    public GamePlayerNotFound(string message) : this(message, TypeError.NotFound)
    {
    }
}