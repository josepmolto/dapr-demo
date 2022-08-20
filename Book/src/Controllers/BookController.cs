using Book.Dto;
using Book.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{

    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    public async Task<IActionResult> BookAsync([FromBody] Request request)
    {
        var response = await _bookService.BookAsync(request).ConfigureAwait(false);

        if (response is Response.Success success)
        {
            return Ok(success);
        }
        else
        {
            var error = response as Response.Error;

            return BadRequest(error);
        }
    }
}
