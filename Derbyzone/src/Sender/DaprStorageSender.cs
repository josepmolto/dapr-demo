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
    private readonly IKeyGenerator _keyGenerator;

    public DaprStorageSender(
        DaprClient daprClient,
        IOptions<SenderOptions> senderOptions,
        IKeyGenerator keyGenerator)
    {
        _daprClient = daprClient;
        _senderOptions = senderOptions.Value;
        _keyGenerator = keyGenerator;
    }

    public Task SendAsync(Offer offer)
    {
        var key = _keyGenerator.GenerateKey(offer);

        Console.WriteLine($"Sending key {key} to dapr");

        return _daprClient.SaveStateAsync(_senderOptions.DaprSenderStoreName, key, offer);
    }
}
