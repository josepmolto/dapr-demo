using Search.Dto;

namespace Search.Services;
public class Searcher : ISearcher
{
    private readonly IRateSearcher _rateSearcher;

    public Searcher(IRateSearcher rateSearcher)
    {
        _rateSearcher = rateSearcher;
    }

    public async Task<Response> SearchAsync(Request request, CancellationToken cancellationToken)
    {
        var availableRates = await _rateSearcher.GetRatesAsync(request, cancellationToken).ConfigureAwait(false);

        var response = new Response()
        {
            Rates = availableRates
        };

        return response;
    }
}