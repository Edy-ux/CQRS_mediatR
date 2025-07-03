using CQRS_mediatR.Application.Interfaces;
using MediatR;

namespace CQRS_mediatR.Application.Features.Player.Notifications;

public class SendWelcomeEmailHandler(IEmailSender emailSender) : INotificationHandler<CreatePlayerNotification>
{
    public async Task Handle(CreatePlayerNotification notification, CancellationToken cancellationToken)
    {

        await emailSender.Sender(notification.playerId, notification.playerEmail);

    }
}