using System;

namespace GamePlayerCQRS.Models.ValueObjects
{
    public enum RoleType { Player, Admin, Moderator, Vip }

    public class PlayerRole
    {
        public string Value { get; private set; }

        public bool IsAdmin => GetRoleType() == RoleType.Admin;
        public bool IsModerator => GetRoleType() == RoleType.Moderator;
        public bool IsVip => GetRoleType() == RoleType.Vip;
        public bool IsPlayer => GetRoleType() == RoleType.Player;

        private PlayerRole(string value)
        {
            Value = value;
        }

        //factory  method for create a new PlayerRole
        public static PlayerRole Create(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role cannot be empty", nameof(role));

            var normalizedRole = role.Trim().ToLower();

            if (!IsValidRole(normalizedRole))
                throw new ArgumentException($"Role '{role}' is not valid", nameof(role));
            return new PlayerRole(role);
        }

        private static bool IsValidRole(string role)
        {
            var validRoles = new[] { "player", "admin", "moderator", "vip" };
            return Array.Exists(validRoles, r => r.Equals(role, StringComparison.OrdinalIgnoreCase));
        }

        private RoleType GetRoleType() => Value.ToLower() switch
        {
            "admin" => RoleType.Admin,
            "moderator" => RoleType.Moderator,
            "vip" => RoleType.Vip,
            "player" => RoleType.Player,
            _ => throw new InvalidOperationException()
        };

        public override bool Equals(object obj)
        {
            if (obj is PlayerRole other)
            {
                return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        public override string ToString()
        {
            return Value;
        }

        public static bool operator ==(PlayerRole left, PlayerRole right)
        {
            return EqualityComparer<PlayerRole>.Default.Equals(left, right);
        }

        public static bool operator !=(PlayerRole left, PlayerRole right)
        {
            return !(left == right);
        }
    }
}