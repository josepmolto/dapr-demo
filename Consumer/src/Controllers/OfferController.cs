using Common.Dto.Derbyzone;
using Consumer.Storage;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace Consumer.Controllers;

[ApiController]
public class OfferController : ControllerBase
{
    private readonly IOfferStorageSender _offerStorageSender;

    public OfferController(IOfferStorageSender offerStorageSender)
    {
        _offerStorageSender = offerStorageSender;
    }

    [Topic("derbyzone", "offer")]
    [HttpPost("/subscribe")]
    public async Task<IActionResult> SuscribeOfferAsync([FromBody] Offer offer)
    {
        await _offerStorageSender.SendAsync(offer).ConfigureAwait(false);

        return Ok();
    }
}