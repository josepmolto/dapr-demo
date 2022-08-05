using Derbyzone.Dto;

namespace Derbyzone.Sender;

public class DummyClientSender : IClientSender
{
    public Task SendAsync(Offer offer)
    {
        Console.WriteLine($"Send offer for hotel {offer.HotelCode} to client");

        return Task.CompletedTask;
    }
}