using System.Text.Json.Serialization;
using CQRS_mediatR.Domain.Validators;
using CQRS_mediatR.Domain.Validators.Exceptions;

namespace CQRS_mediatR.Domain.ValueObjects
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleType
    {
        Player,
        Admin,
        Moderator,
        Vip
    }

    public record PlayerRole

    {
        public RoleType Value { get; }
        public bool IsAdmin => Value is RoleType.Admin;
        public bool IsModerator => Value is RoleType.Moderator;
        public bool IsVip => Value is RoleType.Vip;
        public bool IsPlayer => Value is RoleType.Player;

        private PlayerRole(RoleType type) => Value = type;

        public static PlayerRole Create(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new GamePlayerValidationException("Role cannot be empty");

            return Enum.TryParse<RoleType>(role, true, out var roleType)
                ? new PlayerRole(roleType)
                : throw new GamePlayerValidationException($"Role '{role}' is not valid. Roles allowed: Player Moderator, Admin");
        }

        public override string ToString() => Value.ToString();

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