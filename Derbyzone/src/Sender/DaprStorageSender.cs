using Dapr.Client;
using Derbyzone.Config;
using Derbyzone.Dto;
using Derbyzone.Generators;
using Microsoft.Extensions.Options;

namespace Derbyzone.Sender;
public class DaprStorageSender : IStorageSender
{
    private readonly DaprClient _daprClient;
    private readonly SenderOptions _senderOptions;

    public DaprStorageSender(
        DaprClient daprClient,
        IOptions<SenderOptions> senderOptions)
    {
        _daprClient = daprClient;
        _senderOptions = senderOptions.Value;
    }

    public Task SendAsync(Offer offer)
    {
        Console.WriteLine($"Sending key {offer.Key} to dapr");

        return _daprClient.SaveStateAsync(_senderOptions.DaprSenderStoreName, offer.Key, offer);
    }
}
