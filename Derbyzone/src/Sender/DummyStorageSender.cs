using Derbyzone.Dto;

namespace Derbyzone.Sender;
public class DummyStorageSender : IStorageSender
{
    public Task SendAsync(Offer offer)
    {
        Console.WriteLine($"Send offer for hotel {offer.HotelCode} to storage");

        return Task.CompletedTask;
    }
}
