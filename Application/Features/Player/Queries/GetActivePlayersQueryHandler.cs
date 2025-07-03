
using CSharpFunctionalExtensions;
using CQRS_mediatR.Application.Interfaces;
using CQRS_mediatR.Domain;
using MediatR;
using CQRS_mediatR.Application.Features.Player.Queries;

namespace CQRS_mediatR.Features.Player.Queries;

public class GetActivePlayersQueryHandler(IGamePlayerRepository repository) : IRequestHandler<GetActivePlayersQuery, Result<IEnumerable<GamePlayer>>>

{
    private readonly IGamePlayerRepository _repository = repository;

    public async Task<Result<IEnumerable<GamePlayer>>> Handle(GetActivePlayersQuery request, CancellationToken cancellationToken)
    {
        var players = await _repository.GetActivePlayersAsync();
        return Result.Success(players);
    }

}
