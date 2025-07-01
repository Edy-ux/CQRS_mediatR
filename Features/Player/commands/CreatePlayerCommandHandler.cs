

using CSharpFunctionalExtensions;
using GamePlayerCQRS.Models;
using GamePlayerCQRS.Models.Interfaces;
using MediatR;

namespace GamePlayerCQRS.Features.Player.commands
{
    public class CreatePlayerCommandHandler(IGamePlayerRepository gamePlayerRepository) : IRequestHandler<CreatePlayerCommand, Result<Guid>>
    {
        private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;

        public async Task<Result<Guid>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = GamePlayer.Create(request.dto.Name, request.dto.Email, request.dto.Password, request.dto.Role ?? "Player");

            var result = await _gamePlayerRepository.CreatePlayer(player);

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            return Result.Success(result.Value);
        }
    }
}