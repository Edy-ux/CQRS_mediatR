
using CSharpFunctionalExtensions;
using CQRS_mediatR.Application.Interfaces;
using CQRS_mediatR.Data;
using CQRS_mediatR.Domain;
using CQRS_mediatR.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CQRS_mediatR.Repository;

public class GamePlayerRepository(GamePlayerDbContext context) : IGamePlayerRepository
{
    public async Task<Guid> InsertPlayerAsync(GamePlayer player)
    {
        context.GamePlayers.Add(player);

        await context.SaveChangesAsync();

        return player.Id;
    }

    public async Task<IEnumerable<GamePlayer>> GetActivePlayersAsync()
    {
        var activePlayers = await context.GamePlayers
            .Where(gp => gp.Status == PlayerStatus.Active) // Or g => EF.Property<PlayerStatus>(g, "Status")
            .ToListAsync();

        return activePlayers ?? Enumerable.Empty<GamePlayer>();
    }

    public Task<GamePlayer?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
