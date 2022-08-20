using Dapr.Client;
using Derbyzone.Config;
using Derbyzone.Dto.Book;
using Microsoft.Extensions.Options;

namespace Derbyzone.Book;
public class BookService : IBookService
{
    private const char SEPARATOR = '@';

    private readonly DaprClient _daprClient;
    private readonly DaprOptions _daprOptions;

    public BookService(
        DaprClient daprClient,
        IOptions<DaprOptions> daprOptions)
    {
        _daprClient = daprClient;
        _daprOptions = daprOptions.Value;
    }

    public async Task<Response> BookAsync(Request request)
    {
        var offersKeys = request.BookingKey.Split(SEPARATOR);

        var data = await _daprClient.GetBulkStateAsync(_daprOptions.StoreName, offersKeys, 10);

        if (data.Count != offersKeys.Count() || !AllKeysExist(data))
        {
            return ReturnNotAvailabilityError();
        }

        foreach (var offer in data)
        {
            Console.WriteLine($"Trying to remove offer with key {offer.Key} with etag {offer.ETag}");
            if (!await _daprClient.TryDeleteStateAsync(_daprOptions.StoreName, offer.Key, offer.ETag).ConfigureAwait(false))
            {
                return ReturnNotAvailabilityError();
            }
        }

        return new Response.Success();
    }

    private bool AllKeysExist(IEnumerable<BulkStateItem> items) =>
        items.All(item => !string.IsNullOrWhiteSpace(item.ETag));

    private Response ReturnNotAvailabilityError() =>
        new Response.Error()
        {
            Code = 1,
            ErrorMessage = "Booking not available"
        };
}