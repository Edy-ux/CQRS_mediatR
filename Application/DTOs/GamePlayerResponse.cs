

using CQRS_mediatR.Domain.ValueObjects;

namespace CQRS_mediatR.Application.DTOs
{
    public class GamePlayerResponse
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class GamePlayerDetailResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public PlayerRole Role { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }
        public bool IsSuspended { get; set; }
        public bool IsInactive { get; set; }
    }

    public class GamePlayerListResponse
    {

        public IEnumerable<GamePlayerResponse> Players { get; set; }

    }

    public class GamePlayerStatsResponse
    {
        public int TotalPlayers { get; set; }
        public int ActivePlayers { get; set; }
        public int InactivePlayers { get; set; }
        public int SuspendedPlayers { get; set; }
        public List<RoleStats> PlayersByRole { get; set; } = new();
        public List<StatusStats> PlayersByStatus { get; set; } = new();
    }

    public class RoleStats
    {
        public string Role { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

    public class StatusStats
    {
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Percentage { get; set; }
    }

    public class UpdateGamePlayerResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Message { get; set; } = "Jogador atualizado com sucesso";
    }

    public class DeleteGamePlayerResponse
    {
        public string Message { get; set; } = "Jogador excluído com sucesso";
    }
}