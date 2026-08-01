using EAP.Core;
using Microsoft.AspNetCore.Mvc;

namespace EAP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly EAP.Core.Services.ISimulationEngine _simulationEngine;
        private readonly EAP.Core.Services.ITransientService _transientService;
        private readonly EAP.Core.Services.IScopedService _scopedService;
        private readonly EAP.Core.Services.ISingletonService _singletonService;
        private readonly EAP.Core.Services.ISingletonService _singletonService2;

        public TestController(EAP.Core.Services.ISimulationEngine simulationEngine, Core.Services.ITransientService transientService, Core.Services.IScopedService scopedService, Core.Services.ISingletonService singletonService, Core.Services.ISingletonService singletonService2)
        {
            _simulationEngine = simulationEngine;
            _transientService = transientService;
            _scopedService = scopedService;
            _singletonService = singletonService;
            _singletonService2 = singletonService2;
        }

        [HttpGet("simulate")]
        public IActionResult simulate()
        {
            var response = _simulationEngine.GenerateResponsePacket("Sensor", "{\"temp\": 25}");
            return Ok(response);
        }

        [HttpGet("lifetime")]
        public IActionResult lifetime()
        {
            return Ok(new
            {
                Transient = _transientService.id,
                Scoped = _scopedService.id,
                Singleton = _singletonService.id,
                Singleton2 = _singletonService2.id,
                Note = "Refresh page: Transient changes every time, Scoped same per request, Singleton same forever"
            });
        }

        [HttpGet("linq-demo")]
        public IActionResult linqDemo()
        {
            var devices = new List<Device>
            {
                new Device { Id=1, DeviceName="Sensor-A", DeviceType="Sensor", IsActive=true },
                new Device { Id=2, DeviceName="Actuator-B", DeviceType="Actuator", IsActive=false },
                new Device { Id=3, DeviceName="Sensor-C", DeviceType="Sensor", IsActive=true },
            };

            var activeOnly = devices.Where(d => d.IsActive).ToList();
            var namesOnly = devices.Select(d => d.DeviceName).ToList();
            var sorted = devices.OrderBy(d => d.DeviceName).ToList();
            var anyInactive = devices.Any(d => !d.IsActive);

            // For GroupBy we need to shape it for JSON
            var grouped = devices.GroupBy(d => d.DeviceType)
                                 .Select(g => new { Type = g.Key, Count = g.Count(), Devices = g.ToList() })
                                 .ToList();

            return Ok(new
            {
                AllDevices = devices,
                ActiveOnly = activeOnly,
                NamesOnly = namesOnly,
                SortedByName = sorted,
                AnyInactive = anyInactive,
                GroupedByType = grouped
            });
        }
    }
}