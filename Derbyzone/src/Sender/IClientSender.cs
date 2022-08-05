using Derbyzone.Dto;

namespace Derbyzone.Sender;
public interface IClientSender
{
    Task SendAsync(Offer offer);
}