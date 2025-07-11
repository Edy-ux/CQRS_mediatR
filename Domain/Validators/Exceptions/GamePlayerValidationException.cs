namespace CQRS_mediatR.Domain.Validators.Exceptions;

// Exceções personalizadas Domain

public class GamePlayerValidationException : Exception
{
    public IList<string> Errors { get; }

    public GamePlayerValidationException(IList<string> errors)
        : base(message: "Gameplayer validation errors")
    {
        Errors = errors;
    }

    public GamePlayerValidationException(string error)
        : this(new List<string> { error })
    {

    }
}

public class GamePlayerDomainException : Exception
{
    public GamePlayerDomainException(string message) : base(message) { }
}



// Exceções de Infraestrutura
public class DatabaseException : Exception
{
    public DatabaseException(string message, Exception innerException)
        : base(message, innerException)
    {

    }
}

public class GamePlayerNotFoundException : Exception
{
    public GamePlayerNotFoundException(string message) : base(message) { }
}

public class GamePlayerRepositoryException(string message) : Exception(message);

public class GamePlayerAlreadyExistsException : Exception
{
    public GamePlayerAlreadyExistsException(string email)
        : base($"Already exists player with given email '{email}'.") { }
}