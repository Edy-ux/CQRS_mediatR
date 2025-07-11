using CQRS_mediatR.Application.DTOs;
using CQRS_mediatR.Domain.Entity;
using CQRS_mediatR.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CQRS_mediatR.Infrastructure.Extensions;

public static class PlayerQueryExtension
{
    public static IQueryable<GamePlayer> OnlyActive(this IQueryable<GamePlayer> query)
        => query.Where(p => p.Status == PlayerStatus.Active);

    public static IQueryable<GamePlayer> ByStatus(this IQueryable<GamePlayer> query, PlayerStatus status)
        => query.Where(p => p.Status == status);

    public static IQueryable<GamePlayerDetailResponse> SelectPlayersActive(this IQueryable<GamePlayer> query)
    {
        return query.Select(player => new GamePlayerDetailResponse
        {
            Id = player.Id,
            Name = player.Name,
            Email = player.Email,
            Role = player.Role!,
            Status = player.Status!.ToString(),
            CreatedAt = player.CreatedAt,
            UpdatedAt = player.UpdatedAt,
            IsActive = player.IsActive,
            IsInactive = player.IsInactive,
            IsSuspended = player.IsSuspended
        });
    }

    public static IQueryable<GamePlayerDetailResponse> SelectPlayersOffSetPagination(
        this IQueryable<GamePlayer> query,
        string orderBy = "Name",
        int page = 1,
        int pageSize = 10)
    {
        return query
            .OrderBy(p => p.Name == orderBy)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(player => new GamePlayerDetailResponse
            {
                Id = player.Id,
                Name = player.Name,
                Email = player.Email,
                Role = player.Role!,
                Status = player.Status!.ToString(),
                CreatedAt = player.CreatedAt,
                UpdatedAt = player.UpdatedAt,
                IsActive = player.IsActive,
                IsInactive = player.IsInactive,
                IsSuspended = player.IsSuspended
            });
    }
}