using Common.Dto.Derbyzone;

namespace Consumer.Key;
public class KeyGenerator : IKeyGenerator
{
    private const char SEPARATOR = ':';
    public string Generate(Offer offer)
    {
        var parameters = new string[] {
            offer.HotelCode,
            offer.Date.ToString("yyyy-MM-dd"),
            offer.RoomType
        };

        var key = string.Join(SEPARATOR, parameters);

        return key;
    }
}
