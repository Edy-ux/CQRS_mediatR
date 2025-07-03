
using CQRS_mediatR.Domain;

namespace CQRS_mediatR.Application.Interfaces
{
    public interface IGamePlayerRepository
    {
        Task<Guid> InsertPlayerAsync(GamePlayer player);
        Task<GamePlayer?> GetByIdAsync(Guid id);
        Task<IEnumerable<GamePlayer>> GetActivePlayersAsync();
    }
}