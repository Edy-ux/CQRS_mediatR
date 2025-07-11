using System.Net.Mail;
using CQRS_mediatR.Domain.Validators;
using CQRS_mediatR.Domain.Validators.Exceptions;
using CQRS_mediatR.Domain.ValueObjects;

namespace CQRS_mediatR.Domain.Entity
{
    public class GamePlayer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public PlayerRole? Role { get; private set; }
        public PlayerStatus? Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        //Propriedades de negócio/computadas

        public bool IsActive => Status == PlayerStatus.Active;
        public bool IsSuspended => Status == PlayerStatus.Suspended;
        public bool IsInactive => Status == PlayerStatus.Inactive;


        protected GamePlayer()
        {
        }


        internal GamePlayer(string name, string email, string password, string role)
        {
            Id = Guid.NewGuid();
            Name = name.Trim();
            Email = email.Trim();
            Password = password.Trim();
            Role = PlayerRole.Create(role);
            Status = PlayerStatus.Active;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new GamePlayerDomainException("Nome não pode ser vazio");
            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new GamePlayerDomainException("Email não pode ser vazio");

            var address = new MailAddress(newEmail);

            if (address.Address != newEmail)
                throw new GamePlayerDomainException("Email inválido");

            Email = newEmail;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new GamePlayerDomainException("Senha não pode ser vazia");

            Password = newPassword;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeRole(string newRole)
        {
            Role = PlayerRole.Create(newRole);
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeStatus(string status)
        {
            Status = PlayerStatus.Create(status);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}