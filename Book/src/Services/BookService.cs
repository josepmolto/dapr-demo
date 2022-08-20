using System.Globalization;
using System.Net.Http.Headers;
using System.Text.Json;
using Book.Dto;
using Book.Provider;

namespace Book.Services;
public class BookService : IBookService
{
    private readonly HttpClient _httpClient;
    private readonly IOffersJanitor _offersJanitor;

    public BookService(
        HttpClient httpClient,
        IOffersJanitor offersJanitor)
    {
        _httpClient = httpClient;
        _offersJanitor = offersJanitor;
    }

    public async Task<Response> BookAsync(Request request)
    {
        using var requestMessage = CreateRequestMessage(request);

        using var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);

        if (response.IsSuccessStatusCode)
        {
            await _offersJanitor.RemoveOffersAsync(request).ConfigureAwait(false);

            return new Response.Success();
        }

        var errorResponse = await DeserializeResponseAsync(response);

        return CreateErrorResponse(errorResponse);
    }

    private Response.Error CreateErrorResponse(ProviderResponse errorResponse) =>
        new()
        {
            Code = errorResponse.Code.ToString(CultureInfo.InvariantCulture),
            ErrorMessage = errorResponse.ErrorMessage
        };

    private HttpRequestMessage CreateRequestMessage(Request request)
    {
        var content = JsonSerializer.SerializeToUtf8Bytes(request);
        var memoryStream = new MemoryStream(content);

        var requestMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            Content = new StreamContent(memoryStream),
            RequestUri = new Uri("/book", UriKind.Relative)
        };

        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        return requestMessage;
    }

    private async Task<ProviderResponse> DeserializeResponseAsync(HttpResponseMessage httpResponseMessage)
    {
        using var stream = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);

        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var providerResponse = await JsonSerializer.DeserializeAsync<ProviderResponse>(stream, jsonSerializerOptions).ConfigureAwait(false);

        return providerResponse;
    }
}