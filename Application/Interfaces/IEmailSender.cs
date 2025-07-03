

namespace CQRS_mediatR.Application.Interfaces;

public interface IEmailSender
{
    public Task Sender(Guid playerId, string playerEmail);
}
