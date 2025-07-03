using CSharpFunctionalExtensions;
using CQRS_mediatR.Application.Features.Player.Notifications;
using CQRS_mediatR.Application.Interfaces;
using CQRS_mediatR.Domain;
using MediatR;

namespace CQRS_mediatR.Application.Features.Player.commands
{
    public class CreatePlayerCommandHandler(IGamePlayerRepository gamePlayerRepository, IPublisher publisher)
          : IRequestHandler<CreatePlayerCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var result = GamePlayer.Create(request.dto.Name, request.dto.Email, request.dto.Password,
                request.dto.Role);

            if (!result.IsValid)
            {
                return Result.Failure<Guid>(result.Error);
            }

            var playerId = await gamePlayerRepository.InsertPlayerAsync(result.Value);

            await publisher.Publish(new CreatePlayerNotification(result.Value.Id, result.Value.Email),
                cancellationToken);

            return Result.Success(playerId);
        }
    }
}