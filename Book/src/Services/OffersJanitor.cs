using Book.Config;
using Book.Dto;
using Common.Key;
using Dapr.Client;
using Microsoft.Extensions.Options;

namespace Book.Services;
public class OffersJanitor : IOffersJanitor
{
    private const char SEPARATOR = '@';

    private readonly DaprClient _daprClient;
    private readonly DaprOptions _daprOptions;
    private readonly IKeyGenerator _keyGenerator;

    public OffersJanitor(
        DaprClient daprClient,
        IOptions<DaprOptions> daprOptions,
        IKeyGenerator keyGenerator)
    {
        _daprClient = daprClient;
        _daprOptions = daprOptions.Value;
        _keyGenerator = keyGenerator;
    }

    public async Task RemoveOffersAsync(Request request)
    {
        var offersKey = request.BookingKey.Split(SEPARATOR);

        foreach (var offerKey in offersKey)
        {
            var (hotelCode, roomType, date) = DeconstructDerbyzoneKey(offerKey);
            var key = _keyGenerator.Generate(hotelCode, roomType, date);

            await _daprClient.DeleteStateAsync(_daprOptions.StoreName, key).ConfigureAwait(false);
        }
    }

    private (string hotelCode, string roomtype, DateTime dateTime) DeconstructDerbyzoneKey(string key)
    {
        var parameters = key.Split('|');

        return (parameters[0], parameters[1], DateTime.Parse(parameters[2]));
    }
}