using System;

namespace GamePlayerCQRS.Models.ValueObjects
{

    public enum StatusType { Active, Inactive, Suspended }

    public class PlayerStatus
    {
        public string Value { get; private set; }

        private PlayerStatus(string value)
        {
            Value = value;
        }

        public static PlayerStatus Active => new PlayerStatus(StatusType.Active.ToString());
        public static PlayerStatus Inactive => new PlayerStatus(StatusType.Inactive.ToString());
        public static PlayerStatus Suspended => new PlayerStatus(StatusType.Suspended.ToString());


        // Factory method para criar um novo status
        public static PlayerStatus Create(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status não pode ser vazio", nameof(status));

            var normalizedStatus = status.Trim().ToLower();

            switch (normalizedStatus)
            {
                case "active":
                    return Active;
                case "inactive":
                    return Inactive;
                case "suspended":
                    return Suspended;
                default:
                    throw new ArgumentException($"Status '{status}' não é válido", nameof(status));
            }
        }

        private StatusType GetStatusType() => Value.ToLower() switch
        {
            "active" => StatusType.Active,
            "inactive" => StatusType.Inactive,
            _ => throw new InvalidOperationException()
        };

        public override bool Equals(object obj)
        {
            if (obj is PlayerStatus other)
            {
                return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value.ToLower().GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }

        /*  public static bool operator ==(PlayerStatus left, PlayerStatus right)
         {
             return EqualityComparer<PlayerStatus>.Default.Equals(left, right);
         }

         public static bool operator !=(PlayerStatus left, PlayerStatus right)
         {
             return !(left == right);
         } */
    }
}