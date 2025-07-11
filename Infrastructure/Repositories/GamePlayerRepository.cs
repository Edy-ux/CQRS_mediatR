#pragma warning restore CS8601 // Possible null reference assignment.
#pragma warning restore CS8602 // Dereference of a possibly null reference.


using CQRS_mediatR.Application.DTOs;
using CQRS_mediatR.Domain.Interfaces;
using CQRS_mediatR.Data;
using CQRS_mediatR.Domain.Entity;
using CQRS_mediatR.Domain.Validators.Exceptions;
using CQRS_mediatR.Domain.ValueObjects;
using CQRS_mediatR.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CQRS_mediatR.Repository;

public class GamePlayerRepository(GamePlayerDbContext context) : IGamePlayerRepository
{
    public async Task<Guid> InsertPlayerAsync(GamePlayer player, CancellationToken cancellationToken)
    {
        try
        {
            var exists = await context.GamePlayers
                .AnyAsync(p => p.Email == player.Email, cancellationToken);

            if (exists)
                throw new GamePlayerAlreadyExistsException(player.Email);
            context.GamePlayers.Add(player);
            await context.SaveChangesAsync(cancellationToken);
            return player.Id;
        }
        catch (Exception ex) when (ex is not GamePlayerAlreadyExistsException)
        {
            throw new DatabaseException(
                "Error while creation gameplayer on database", ex);
        }
    }

    public async Task<IEnumerable<GamePlayerDetailResponse>> GetActivePlayersAsync()
    {
        try
        {
            var activePlayers = await context.GamePlayers
                .OnlyActive()
                .SelectPlayersActive()
                .ToListAsync();

            return activePlayers ?? throw new GamePlayerNotFoundException($"Cannot find any active players.");
        }
        catch (Exception ex) when (ex is not GamePlayerNotFoundException)
        {
            throw new DatabaseException(
                "An error occurred while processing you request", ex);
        }
    }


    public async Task<GamePlayer?> GetByIdAsync(Guid id)
    {
        try
        {
            var player = await context.GamePlayers
                .FirstOrDefaultAsync(p => p.Id == id);
            if (player == null)
                throw new GamePlayerNotFoundException($"GamePlayer com ID {id} não encontrado");

            return player;
        }
        catch (Exception ex) when (ex is not GamePlayerNotFoundException)
        {
            throw new DatabaseException(
                "Error when searching for gameplayer from the database", ex);
        }
    }
}