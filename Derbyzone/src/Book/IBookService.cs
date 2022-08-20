using Derbyzone.Dto.Book;

namespace Derbyzone.Book;
public interface IBookService
{
    Task<Response> BookAsync(Request request);
}