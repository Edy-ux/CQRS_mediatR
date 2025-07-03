

using CQRS_mediatR.Application.Interfaces;

namespace CQRS_mediatR.Infrastructure.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public async Task Sender(Guid playerId, string playerEmail)
        {
            Console.WriteLine($"📧 E-mail de boas-vindas enviado para os usuario com ID: {playerId}...");
            await Task.CompletedTask;
        }
    }
}