using Microsoft.AspNetCore.Mvc;

namespace EAP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly EAP.Core.Services.ISimulationEngine _simulationEngine;

        public TestController(EAP.Core.Services.ISimulationEngine simulationEngine)
        {
            _simulationEngine = simulationEngine;
        }

        [HttpGet("simulate")]
        public IActionResult simulate()
        {
            var response = _simulationEngine.GenerateResponsePacket("Sensor", "{\"temp\": 25}");
            return Ok(response);
        }
    }
}