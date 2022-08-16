using Search.Dto;

namespace Search.Services;
public interface IRateSearcher
{
    Task<IEnumerable<Rate>> GetRatesAsync(Request request, CancellationToken cancellationToken);
}