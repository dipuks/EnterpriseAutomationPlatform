using EAP.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace EAP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly DeviceRepository _deviceRepository;

        public DevicesController(DeviceRepository deviceRepository) => _deviceRepository = deviceRepository;

        [HttpGet]
        public IActionResult Get() => Ok(_deviceRepository.GetAll);

        [HttpGet("online")]
        public IActionResult GetOnline() => Ok(_deviceRepository.GetOnline);

        [HttpPost]
        public IActionResult Post(EAP.Core.Device device)
        {
            device.LastSeen = DateTime.UtcNow;
            return Ok(_deviceRepository.Add(device));
        }

    }
}
