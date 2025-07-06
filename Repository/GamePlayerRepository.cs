using CQRS_mediatR.Application.DTOs;
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

    public async Task<IEnumerable<GamePlayerDetailResponse>> GetActivePlayersAsync()
    {
        var activePlayers = await context.GamePlayers
            .Where(p => p.Status == PlayerStatus.Active) // Or g => 
            .ToListAsync();

        return activePlayers.Select(player => new GamePlayerDetailResponse
        {
            Id = player.Id,
            Name = player.Name,
            Email = player.Email,
            Role = player.Role.Value,
            Status = player.Status.ToString(),
            CreatedAt = player.CreatedAt,
            UpdatedAt = player.UpdatedAt,
            IsActive = player.Status == PlayerStatus.Active,
            IsInactive = player.Status == PlayerStatus.Inactive,    
            IsSuspended = player.Status == PlayerStatus.Suspended
        });
    }

    public Task<GamePlayer?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}