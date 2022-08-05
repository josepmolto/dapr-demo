using Producer.Config;
using Dapr.Client;
using Microsoft.Extensions.Options;
using Common.Dto.Derbyzone;

namespace Producer.Sender;

public class DaprQueueSender : IQueueSender
{
    private readonly DaprClient _daprClient;
    private readonly QueueConfiguration _queueConfiguration;

    public DaprQueueSender(
        DaprClient daprClient,
        IOptions<QueueConfiguration> queueConfiguration)
    {
        _daprClient = daprClient;
        _queueConfiguration = queueConfiguration.Value;
    }

    public async Task SendAsync(Offer offer, CancellationToken cancellationToken)
    {

        await _daprClient.PublishEventAsync(
            _queueConfiguration.PubSubName,
            _queueConfiguration.TopicName,
            offer,
            cancellationToken).ConfigureAwait(false);

        Console.WriteLine($"Offer for hotel {offer.HotelCode} processed");
    }
}