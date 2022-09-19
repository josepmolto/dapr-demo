using Derbyzone.Config;
using Derbyzone.Dto;
using Microsoft.Extensions.Options;
using Derbyzone.Extensions;


namespace Derbyzone.Generators;
public class OfferGenerator : IOfferGenerator
{
    private readonly RandomData _randomData;
    private readonly Random _random;
    private readonly IEnumerable<DateTime> _dates;
    private readonly IKeyGenerator _keyGenerator;

    public OfferGenerator(IOptions<RandomData> randomData, IKeyGenerator keyGenerator)
    {
        _randomData = randomData.Value;
        _dates = GetNext30Days();
        _keyGenerator = keyGenerator;
    }

    public Offer GenerateOffer()
    {
        var date = RandomHelper.Next(_dates);

        var offer = new Offer()
        {
            Date = date,
            HotelCode = RandomHelper.Next(_randomData.Hotels),
            RoomType = RandomHelper.Next(_randomData.RoomTypes),
            Cost = Random.Shared.Next(50, 2501),
            CancellationPolicies = GetRandomCancellationPolicy(date)
        };

        offer.Key = _keyGenerator.GenerateKey(offer);

        return offer;
    }

    private static IEnumerable<DateTime> GetNext30Days()
    {
        for (var days = 1; days <= 30; days++)
        {
            yield return DateTime.Today.AddDays(days);
        }
    }

    private IEnumerable<CancellationPolicy> GetRandomCancellationPolicy(DateTime date)
    {
        if (date < DateTime.Today.AddDays(5))
        {
            return new[] { NonRefundable() };
        }

        var randomPenalties = new byte[] { 25, 50, 75, 100 };
        var cancellationPolicies = new List<CancellationPolicy>();

        var firstCancellationPolicy = new CancellationPolicy()
        {
            DateFrom = DateTime.Today,
            PenaltyPercentage = RandomHelper.Next(randomPenalties)
        };

        cancellationPolicies.Add(firstCancellationPolicy);

        if (firstCancellationPolicy.PenaltyPercentage != 100)
        {
            var secondPolicyPenalty = (byte)100;
            do
            {
                secondPolicyPenalty = RandomHelper.Next(randomPenalties);
            }
            while (secondPolicyPenalty <= firstCancellationPolicy.PenaltyPercentage);

            cancellationPolicies.Add(new CancellationPolicy()
            {
                DateFrom = firstCancellationPolicy.DateFrom.AddDays(3),
                PenaltyPercentage = secondPolicyPenalty
            });
        }

        return cancellationPolicies;

        CancellationPolicy NonRefundable() =>
            new()
            {
                DateFrom = DateTime.Today,
                PenaltyPercentage = 100
            };
    }
}
