using Book.Dto;

namespace Book.Services;
public interface IOffersJanitor
{
    Task RemoveOffersAsync(Request request);
}