using Dapr.Client;
using Derbyzone.Config;
using Derbyzone.Dto;
using Derbyzone.Generators;
using Microsoft.Extensions.Options;

namespace Derbyzone.Sender;
public class DaprStorageSender : IStorageSender
{
    private readonly DaprClient _daprClient;
    private readonly DaprOptions _daprOptions;

    public DaprStorageSender(
        DaprClient daprClient,
        IOptions<DaprOptions> daprOptions)
    {
        _daprClient = daprClient;
        _daprOptions = daprOptions.Value;
    }

    public Task SendAsync(Offer offer)
    {
        Console.WriteLine($"Sending key {offer.Key} to dapr");

        return _daprClient.SaveStateAsync(_daprOptions.StoreName, offer.Key, offer);
    }
}
