using CQRS_mediatR.Application.DTOs;
using CQRS_mediatR.Application.Errors;
using CSharpFunctionalExtensions;
using CQRS_mediatR.Domain.Interfaces;
using OneOf;
using MediatR;
using CQRS_mediatR.Application.Features.Player.Queries;
using CQRS_mediatR.Domain.Validators.Exceptions;

namespace CQRS_mediatR.Features.Player.Queries;

public class GetActivePlayersQueryHandler(IGamePlayerRepository repository)
    : IRequestHandler<GetActivePlayersQuery, OneOf<IEnumerable<GamePlayerDetailResponse>, GamePlayerNotFound>>
{
    public async Task<OneOf<IEnumerable<GamePlayerDetailResponse>, GamePlayerNotFound>> Handle(
        GetActivePlayersQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var players = await repository.GetActivePlayersAsync();
            return players.ToList();
        }
        catch (GamePlayerNotFoundException ex)
        {
            return new GamePlayerNotFound
                (ex.Message);
        }
    }
}