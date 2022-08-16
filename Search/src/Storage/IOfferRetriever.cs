using Common.Dto.Derbyzone;

namespace Search.Storage;
public interface IOfferRetriever
{
    Task<Offer> GetOfferAsync(string key, CancellationToken cancellationToken);
}