using Microsoft.AspNetCore.Mvc;

namespace Derbyzone.Controllers;
public class OrchestratorController : ControllerBase
{
    private readonly IOffersOrchestrator _offersOrchestrator;

    public OrchestratorController(IOffersOrchestrator offersOrchestrator)
    {
        this._offersOrchestrator = offersOrchestrator;
    }

    [HttpPost("generate_offers")]
    public async Task<IActionResult> GenerateOffers()
    {
        await _offersOrchestrator.OrchestrateAsync().ConfigureAwait(false);

        return Ok();
    }

}