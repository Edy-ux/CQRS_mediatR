
using CQRS_mediatR.Application.DTOs;
using CQRS_mediatR.Domain.Entity;

namespace CQRS_mediatR.Domain.Interfaces
{
    public interface IGamePlayerRepository
    {
        Task<Guid> InsertPlayerAsync(GamePlayer player, CancellationToken cancellationToken);
        Task<GamePlayer?> GetByIdAsync(Guid id);
        Task<IEnumerable<GamePlayerDetailResponse>> GetActivePlayersAsync();
    }
}