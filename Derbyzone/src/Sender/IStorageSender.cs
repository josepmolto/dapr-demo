using Derbyzone.Dto;

namespace Derbyzone.Sender;
public interface IStorageSender
{
    Task SendAsync(Offer offer);
}