
using CSharpFunctionalExtensions;
using GamePlayerCQRS.Data;
using GamePlayerCQRS.Models;
using GamePlayerCQRS.Models.Interfaces;
using GamePlayerCQRS.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GamePlayerCQRS.Repository;

public class GamePlayerRepository(GamePlayerDbContext context) : IGamePlayerRepository
{
    public readonly GamePlayerDbContext _context = context;

    public async Task<Result<Guid>> InsertPlayerAsync(GamePlayer player)
    {
        _context.GamePlayers.Add(player);

        await _context.SaveChangesAsync();

        return Result.Success(player.Id);
    }

    public async Task<Result<IEnumerable<GamePlayer>>> GetActivePlayersAsync()
    {
        try
        {
            var activePlayers = await _context.GamePlayers
                .Where(gp => gp.Status == PlayerStatus.Active) // Or g => EF.Property<PlayerStatus>(g, "Status")
                .ToListAsync();

            return Result.Success<IEnumerable<GamePlayer>>(activePlayers);
        }
        catch (Exception ex)

        {
            return Result.Failure<IEnumerable<GamePlayer>>($"Error to get active player: {ex.Message}");
        }
    }

    public Task<GamePlayer?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
