using Search.Dto;

namespace Search.Services;
public interface ISearcher
{
    Task<Response> SearchAsync(Request request, CancellationToken cancellationToken);
}