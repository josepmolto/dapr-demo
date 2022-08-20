using Book.Dto;

namespace Book.Services;
public interface IBookService
{
    Task<Response> BookAsync(Request request);
}