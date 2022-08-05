using Derbyzone.Dto;

namespace Derbyzone.Generators;
public interface IKeyGenerator
{
    string GenerateKey(Offer offer);
}