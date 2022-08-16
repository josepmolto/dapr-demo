using Common.Dto.Derbyzone;
using Dapr.Client;
using Microsoft.Extensions.Options;
using Search.Config;

namespace Search.Storage;
public class DaprOfferRetriever : IOfferRetriever
{
    private readonly DaprClient _daprClient;
    private readonly DaprOptions _daprOptions;

    public DaprOfferRetriever(
        DaprClient daprClient,
        IOptions<DaprOptions> daprOptions)
    {
        _daprClient = daprClient;
        _daprOptions = daprOptions.Value;
    }

    public Task<Offer> GetOfferAsync(
        string key,
        CancellationToken cancellationToken)
    {
        return _daprClient.GetStateAsync<Offer>(_daprOptions.StoreName, key, cancellationToken: cancellationToken);
    }
}