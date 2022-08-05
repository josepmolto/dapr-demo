using Common.Dto.Derbyzone;

namespace Consumer.Key;
public interface IKeyGenerator
{
    string Generate(Offer offer);
}