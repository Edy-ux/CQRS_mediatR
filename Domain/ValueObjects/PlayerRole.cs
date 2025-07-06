using CQRS_mediatR.Domain.Validators;

namespace CQRS_mediatR.Domain.ValueObjects
{
    public enum RoleType
    {
        Player,
        Admin,
        Moderator,
        Vip
    }

    public record PlayerRole
    {
        public static RoleType Type { get; private set; }

        public string Value { get;} = Type.ToString();

        public bool IsAdmin => Type == RoleType.Admin;
        public bool IsModerator => Type == RoleType.Moderator;
        public bool IsVip => Type == RoleType.Vip;
        public bool IsPlayer => Type == RoleType.Player;

        private PlayerRole(RoleType type) => Type = type;


        //factory  method for create a new PlayerRole
        public static PlayerRole Create(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty", nameof(role));

            return Enum.TryParse<RoleType>(role, true, out var roleType)
                ? new PlayerRole(roleType)
                : throw new ArgumentException($"Role '{role}' is not valid", nameof(role));
        }

        public override string ToString()
        {
            return Type.ToString();
        }

        /*public static bool operator ==(PlayerRole left, PlayerRole right)
        {
            return EqualityComparer<PlayerRole>.Default.Equals(left, right);
        }

        public static bool operator !=(PlayerRole left, PlayerRole right)
        {
            return !(left == right);
        }
        */
    }
}