using System.Globalization;
using Derbyzone.Dto;

namespace Derbyzone.Generators;

public class KeyGenerator : IKeyGenerator
{
    public string GenerateKey(Offer offer) =>
        string.Join('|',
            offer.HotelCode,
            offer.RoomType,
            offer.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            offer.Cost);
}
