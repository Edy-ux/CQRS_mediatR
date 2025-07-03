using MediatR;

namespace CQRS_mediatR.Application.Features.Player.Notifications;

public record CreatePlayerNotification(Guid playerId, string playerEmail) : INotification;