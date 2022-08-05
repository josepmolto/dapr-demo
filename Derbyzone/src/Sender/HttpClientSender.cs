using Derbyzone.Config;
using Derbyzone.Dto;
using Microsoft.Extensions.Options;

namespace Derbyzone.Sender;
public class HttpClientSender : IClientSender
{
    private readonly HttpClient _httpClient;
    private readonly SenderOptions _senderOptions;

    public HttpClientSender(
        HttpClient httpClient,
        IOptions<SenderOptions> senderOptions)
    {
        _httpClient = httpClient;
        _senderOptions = senderOptions.Value;
    }

    public Task SendAsync(Offer offer)
    {
        Console.WriteLine($"Sending offer for hotel {offer.HotelCode} to client");

        return _httpClient.PostAsJsonAsync(
            new Uri(_senderOptions.ClientHost),
            offer);
    }
}
