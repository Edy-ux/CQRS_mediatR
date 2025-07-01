
using GamePlayerCQRS.Models.ValueObjects;

namespace GamePlayerCQRS.Models
{
    public class GamePlayer
    {

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public PlayerRole Role { get; private set; }
        public PlayerStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        //Propriedades de negócio/computadas
        public bool IsActive => Status == PlayerStatus.Active;
        public bool IsSuspended => Status == PlayerStatus.Suspended;
        public bool IsInactive => Status == PlayerStatus.Inactive;

        // Construtor privado para garantir criação  através de factory methods apenas
        private GamePlayer() { }

        // Factory method para criar um novo jogador
        public static GamePlayer Create(string name, string email, string password, string role = "Player")
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome não pode ser vazio", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email não pode ser vazio", nameof(email));


            return new GamePlayer
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email,
                Password = password,
                Role = PlayerRole.Create(role),
                Status = PlayerStatus.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }

        // Métodos de negócio
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
            Role = PlayerRole.Create(newRole);
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