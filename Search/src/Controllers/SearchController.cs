using Microsoft.AspNetCore.Mvc;
using Search.Dto;
using Search.Services;

namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{

    private readonly ISearcher _searcher;

    public SearchController(ISearcher searcher)
    {
        _searcher = searcher;
    }

    [HttpGet]
    public async Task<IActionResult> GetRatesAsync(
        [FromBody] Request request,
        CancellationToken cancellationToken)
    {
        var response = await _searcher.SearchAsync(request, cancellationToken).ConfigureAwait(false);

        return Ok(response);
    }
}
