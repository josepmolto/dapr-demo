using Common.Dto.Derbyzone;
using Common.Key;
using Microsoft.Extensions.Options;
using Search.Config;
using Search.Dto;
using Search.Storage;

namespace Search.Services;
public class RateSearcher : IRateSearcher
{
    private const char SEPARATOR = '@';

    private readonly IOfferRetriever _offerRetriever;
    private readonly StaticData _staticData;
    private readonly IKeyGenerator _keyGenerator;

    public RateSearcher(
        IOfferRetriever offerRetriever,
        IOptions<StaticData> staticData,
        IKeyGenerator keyGenerator)
    {
        _offerRetriever = offerRetriever;
        _staticData = staticData.Value;
        _keyGenerator = keyGenerator;
    }

    public async Task<IEnumerable<Rate>> GetRatesAsync(Request request, CancellationToken cancellationToken)
    {
        var stayDates = CalculateStayRange(request);
        var offersRate = new List<Rate>();


        foreach (var roomtype in _staticData.RoomTypes)
        {
            var (isValid, offers) = await TryGetOffersAsync(request, roomtype, stayDates, cancellationToken).ConfigureAwait(false);

            if (isValid)
            {
                var offerRate = CreateRate(offers);

                offersRate.Add(offerRate);
            }
        }

        return offersRate;
    }

    private Rate CreateRate(IEnumerable<Offer> offers)
    {
        var firstOffer = offers.First();

        return new()
        {
            CancellationPolicies = firstOffer.CancellationPolicies.Select(cxp => new Dto.CancellationPolicy()
            {
                From = cxp.DateFrom,
                PenaltyPercentage = cxp.PenaltyPercentage
            }),
            Cost = offers.Sum(o => o.Cost),
            RoomType = firstOffer.RoomType,
            BookingKey = string.Join(SEPARATOR, offers.Select(o => o.Key))
        };
    }

    private async Task<(bool isValid, IEnumerable<Offer> offers)> TryGetOffersAsync(
        Request request,
        string roomType,
        IEnumerable<DateTime> stayDates,
        CancellationToken cancellationToken)
    {
        var offers = new List<Offer>(stayDates.Count());

        foreach (var date in stayDates)
        {
            var key = _keyGenerator.Generate(request.HotelCode, roomType, date);

            var offer = await _offerRetriever.GetOfferAsync(key, cancellationToken).ConfigureAwait(false);

            if (offer == default)
            {
                return (false, Enumerable.Empty<Offer>());
            }

            if (offers.Any())
            {
                var cancellationPolicies = offers?.FirstOrDefault()?.CancellationPolicies;

                if (!AreCancellationPoliciesEqual(cancellationPolicies, offer.CancellationPolicies))
                {
                    return (false, Enumerable.Empty<Offer>());
                }
            }


            offers.Add(offer);
        }


        return (true, offers);
    }

    private static IEnumerable<DateTime> CalculateStayRange(Request request)
    {
        for (var date = request.CheckIn; date < request.CheckOut; date = date.AddDays(1))
        {
            yield return date;
        }
    }

    private static bool AreCancellationPoliciesEqual(
        IEnumerable<Common.Dto.Derbyzone.CancellationPolicy> cancellationPolicies,
        IEnumerable<Common.Dto.Derbyzone.CancellationPolicy> newCancellationPolicies)
    {
        if (cancellationPolicies.Count() != newCancellationPolicies.Count())
        {
            return false;
        }

        for (var idx = 0; idx < cancellationPolicies.Count(); idx++)
        {
            if (cancellationPolicies.ElementAt(idx) != newCancellationPolicies.ElementAt(idx))
            {
                return false;
            }
        }

        return true;
    }
}