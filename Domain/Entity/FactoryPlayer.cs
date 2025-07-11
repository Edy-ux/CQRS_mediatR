
using CQRS_mediatR.Domain.Validators.Exceptions;

namespace CQRS_mediatR.Domain.Entity;

public class FactoryPlayer : GamePlayer
{
    private static readonly string[] AllowedRoles = ["Player", "Moderator", "Admin"];
    public static GamePlayer Create(string name, string email, string password, string role = "Player")
    {
        List<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(role))
            errors.Add($"Role '{role}' is not valid. Allowed roles: {string.Join(", ", AllowedRoles)}");
        if (string.IsNullOrWhiteSpace(name))
            errors.Add("Name cannot be empty");
        if (errors.Any())

            throw new GamePlayerValidationException(errors);

        return new GamePlayer(name, email, password, role);
    }
}