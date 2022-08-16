using Common.Dto.Derbyzone;

namespace Common.Key;
public class KeyGenerator : IKeyGenerator
{
    private const char SEPARATOR = ':';

    public string Generate(Offer offer) =>
        Generate(offer.HotelCode, offer.RoomType, offer.Date);

    public string Generate(string hotelCode, string roomType, DateTime date)
    {
        var parameters = new string[] {
            hotelCode,
            date.ToString("yyyy-MM-dd"),
            roomType
        };

        var key = string.Join(SEPARATOR, parameters);

        return key;
    }
}
