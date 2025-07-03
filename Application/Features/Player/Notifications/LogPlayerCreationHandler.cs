using MediatR;

namespace CQRS_mediatR.Application.Features.Player.Notifications;

public class LogPlayerCreationHandler(ILogger<LogPlayerCreationHandler> logger)
    : INotificationHandler<CreatePlayerNotification>
{
    public Task Handle(CreatePlayerNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Sending email to {notification.playerEmail}...");
        Task.Delay(1000);
        logger.LogInformation($"Send email to {notification.playerEmail} done");

        return Task.CompletedTask;
    }
}