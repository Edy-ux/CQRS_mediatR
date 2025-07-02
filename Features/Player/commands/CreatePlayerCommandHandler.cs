

using CSharpFunctionalExtensions;
using GamePlayerCQRS.Features.Player.Notifications;
using GamePlayerCQRS.Models;
using GamePlayerCQRS.Models.Interfaces;
using MediatR;

namespace GamePlayerCQRS.Features.Player.commands
{
    public class CreatePlayerCommandHandler(IGamePlayerRepository gamePlayerRepository, IPublisher publisher) : IRequestHandler<CreatePlayerCommand, Result<Guid>>
    {
        private readonly IPublisher _publisher = publisher;
        private readonly IGamePlayerRepository _gamePlayerRepository = gamePlayerRepository;

        public async Task<Result<Guid>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var player = GamePlayer.Create(request.dto.Name, request.dto.Email, request.dto.Password, request.dto.Role ?? "Player");

            var result = await _gamePlayerRepository.InsertPlayerAsync(player);

            if (result.IsFailure)
                return Result.Failure<Guid>(result.Error);

            await _publisher.Publish(new CreatePlayerNotification(result.Value, request.dto.Email), cancellationToken);

            return Result.Success(result.Value);
        }
    }
}