
using CSharpFunctionalExtensions;
using GamePlayerCQRS.Data;
using GamePlayerCQRS.Models;
using GamePlayerCQRS.Models.Interfaces;

namespace GamePlayerCQRS.Repository;

public class GamePlayerRepository(GamePlayerDbContext context) : IGamePlayerRepository
{
    public readonly GamePlayerDbContext _context = context;

    public async Task<Result<Guid>> CreatePlayer(GamePlayer player)
    {
        _context.GamePlayers.Add(player);

        await _context.SaveChangesAsync();

        return Result.Success(player.Id);
    }
}
