
using CSharpFunctionalExtensions;

namespace GamePlayerCQRS.Models.Interfaces
{
    public interface IGamePlayerRepository
    {
        Task<Result<Guid>> InsertPlayerAsync(GamePlayer player);
        Task<GamePlayer?> GetByIdAsync(Guid id);
        Task<Result<IEnumerable<GamePlayer>>> GetActivePlayersAsync();
    }
}