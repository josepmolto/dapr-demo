
using Common.Dto.Derbyzone;

namespace Producer.Sender;
public interface IQueueSender
{
    Task SendAsync(Offer offer, CancellationToken cancellationToken);
}