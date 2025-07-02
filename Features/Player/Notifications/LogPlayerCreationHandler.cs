

using MediatR;

namespace GamePlayerCQRS.Features.Player.Notifications;

public class LogPlayerCreationHandler : INotificationHandler<CreatePlayerNotification>
{
    private readonly ILogger<LogPlayerCreationHandler> _logger;
    public LogPlayerCreationHandler(ILogger<LogPlayerCreationHandler> logger)
    {
        _logger = logger;

    }

    public async Task Handle(CreatePlayerNotification notification, CancellationToken cancellationToken)
    {

        _logger.LogInformation($"Player created with {notification.playerId} e email: {notification.playerEmail} ");

        await Task.CompletedTask;
    }
}
