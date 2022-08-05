using Common.Dto.Derbyzone;

namespace Consumer.Storage;
public interface IOfferStorageSender
{
    Task SendAsync(Offer offer);
}