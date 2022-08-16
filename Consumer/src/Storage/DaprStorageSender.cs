using Common.Dto.Derbyzone;
using Common.Key;
using Consumer.Config;
using Dapr.Client;
using Microsoft.Extensions.Options;

namespace Consumer.Storage;
public class DaprStorageSender : IOfferStorageSender
{
    private readonly DaprClient _daprClient;
    private readonly IKeyGenerator _keyGenerator;
    private readonly DaprOptions _daprOptions;

    public DaprStorageSender(
        DaprClient daprClient,
        IKeyGenerator keyGenerator,
        IOptions<DaprOptions> daprOptions)
    {
        _daprClient = daprClient;
        _keyGenerator = keyGenerator;
        _daprOptions = daprOptions.Value;
    }

    public async Task SendAsync(Offer offer)
    {
        var key = _keyGenerator.Generate(offer);

        await _daprClient.SaveStateAsync(_daprOptions.StoreName, key, offer).ConfigureAwait(false);
    }
}