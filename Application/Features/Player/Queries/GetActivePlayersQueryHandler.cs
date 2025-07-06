
using CQRS_mediatR.Application.DTOs;
using CSharpFunctionalExtensions;
using CQRS_mediatR.Application.Interfaces;
using CQRS_mediatR.Domain;
using MediatR;
using CQRS_mediatR.Application.Features.Player.Queries;

namespace CQRS_mediatR.Features.Player.Queries;

public class GetActivePlayersQueryHandler(IGamePlayerRepository repository) : IRequestHandler<GetActivePlayersQuery, Result<IEnumerable<GamePlayerDetailResponse>>>

{

    public async Task<Result<IEnumerable<GamePlayerDetailResponse>>> Handle(GetActivePlayersQuery request, CancellationToken cancellationToken)
    {
        var players = await repository.GetActivePlayersAsync();
        return Result.Success(players);
    }

}
