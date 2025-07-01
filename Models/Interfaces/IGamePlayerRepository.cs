
using CSharpFunctionalExtensions;

namespace GamePlayerCQRS.Models.Interfaces
{
    public interface IGamePlayerRepository
    {
        Task<Result<Guid>> CreatePlayer(GamePlayer player);
    }
}