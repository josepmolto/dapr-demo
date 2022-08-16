using Common.Dto.Derbyzone;

namespace Common.Key;
public interface IKeyGenerator
{
    string Generate(Offer offer);
    string Generate(
        string hotelCode,
        string roomType,
        DateTime date);
}