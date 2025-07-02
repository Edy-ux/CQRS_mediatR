
using CSharpFunctionalExtensions;
using GamePlayerCQRS.Models;
using GamePlayerCQRS.Models.Interfaces;
using MediatR;

namespace GamePlayerCQRS.Features.Player.Queries
{
    public class GetActivePlayersQueryHandler(IGamePlayerRepository repository) : IRequestHandler<GetActivePlayersQuery, Result<IEnumerable<GamePlayer>>>
    {
        private readonly IGamePlayerRepository _repository = repository;

        public async Task<Result<IEnumerable<GamePlayer>>> Handle(GetActivePlayersQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetActivePlayersAsync();
        }
    }
}