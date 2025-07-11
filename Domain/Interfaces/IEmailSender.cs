

namespace CQRS_mediatR.Domain.Interfaces;

public interface IEmailSender
{
    public Task Sender(Guid playerId, string playerEmail);
}
