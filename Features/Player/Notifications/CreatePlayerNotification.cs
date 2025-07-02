

using MediatR;

namespace GamePlayerCQRS.Features.Player.Notifications
{
    public record CreatePlayerNotification(Guid playerId, string playerEmail) : INotification;
}