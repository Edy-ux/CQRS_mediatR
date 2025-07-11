using CQRS_mediatR.Application.Errors;
using CQRS_mediatR.Application.Features.Player.Notifications;
using CQRS_mediatR.Domain.Entity;
using CQRS_mediatR.Domain.Interfaces;
using CQRS_mediatR.Domain.Validators.Exceptions;
using MediatR;
using OneOf;

namespace CQRS_mediatR.Application.Features.Player.commands
{
    public class CreatePlayerCommandHandler(IGamePlayerRepository gamePlayerRepository, IPublisher publisher)
        : IRequestHandler<CreatePlayerCommand, OneOf<Guid, CreationPlayerError>>
    {
        public async Task<OneOf<Guid, CreationPlayerError>> Handle(CreatePlayerCommand request,
            CancellationToken ctx)
        {
            try
            {
                var player = FactoryPlayer.Create(request.dto.Name, request.dto.Email, request.dto.Password, request.dto.Role);
                var playerId = await gamePlayerRepository.InsertPlayerAsync(player, ctx);
                await publisher.Publish(new CreatePlayerNotification(playerId, request.dto.Email));
                return playerId;

            }
            catch (GamePlayerAlreadyExistsException ex)
            {
                return new CreationPlayerError(
                 ex.Message,
                 TypeError.Conflict);
            }


        }
    }
}