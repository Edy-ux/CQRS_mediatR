using System;

namespace CQRS_mediatR.Domain.ValueObjects
{
    public enum StatusType
    {
        Active,
        Inactive,
        Suspended
    }

    public record PlayerStatus
    {
        public StatusType Type { get; }
        public static PlayerStatus Active => new(StatusType.Active);
        public static PlayerStatus Inactive => new(StatusType.Inactive);
        public static PlayerStatus Suspended => new(StatusType.Suspended);

        private PlayerStatus(StatusType status) => Type = status;

        // Factory method para criar um novo status
        public static PlayerStatus Create(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status não pode ser vazio", nameof(status));

            return Enum.TryParse<StatusType>(status, true, out var statusType)
                ? new PlayerStatus(statusType)
                : throw new ArgumentException($"Status '{status}' não é válido", nameof(status));
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}