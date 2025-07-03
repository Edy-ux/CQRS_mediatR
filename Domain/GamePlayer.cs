
using CQRS_mediatR.Domain.Validators;
using CQRS_mediatR.Domain.ValueObjects;

namespace CQRS_mediatR.Domain
{
    public class GamePlayer
    {

        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public PlayerRole Role { get; private set; }
        public PlayerStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        //Propriedades de negócio/computadas

        public bool IsActive => Status == PlayerStatus.Active;
        public bool IsSuspended => Status == PlayerStatus.Suspended;
        public bool IsInactive => Status == PlayerStatus.Inactive;

        protected GamePlayer() { }

        // Factory method para criar um novo jogadors
        public static Result<GamePlayer> Create(string name, string email, string password, string role = "Player")
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result<GamePlayer>.Failure(string.Format("Nome não pode ser vazio: {0}", role));

            if (string.IsNullOrWhiteSpace(email))
                return Result<GamePlayer>.Failure(string.Format("Email não pode ser vazio: {0}", role));

            var playerRole = PlayerRole.Create(role);

            if (!playerRole.IsValid)
                return Result<GamePlayer>.Failure(string.Format(playerRole.Error!));

            return Result<GamePlayer>.Success(new GamePlayer
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password,
                Role = PlayerRole.Create(role).Value,
                Status = PlayerStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Nome não pode ser vazio", nameof(newName));

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email não pode ser vazio", nameof(newEmail));

            var address = new System.Net.Mail.MailAddress(newEmail);

            if (address.Address != newEmail)
                throw new ArgumentException("Email inválido", nameof(newEmail));

            Email = newEmail;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Senha não pode ser vazia", nameof(newPassword));

            Password = newPassword;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeRole(string newRole)
        {
            Role = PlayerRole.Create(newRole).Value;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            Status = PlayerStatus.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            Status = PlayerStatus.Inactive;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Suspend()
        {
            Status = PlayerStatus.Suspended;
            UpdatedAt = DateTime.UtcNow;
        }


    }
}