

using MediatR;

namespace GamePlayerCQRS.Features.Player.Notifications
{

    public class SendWelcomeEmailHandler : INotificationHandler<CreatePlayerNotification>
    {
        public async Task Handle(CreatePlayerNotification notification, CancellationToken cancellationToken)
        {
            // Simula envio de e-mail
            Console.WriteLine($"📧 Enviando e-mail de boas-vindas para {notification.playerEmail}...");
            await Task.CompletedTask;
        }
    }
}