using EAP.Core.Data;
using EAP.Core.DTOs;
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
        public IActionResult Post(CreateDeviceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(_deviceRepository.Add(dto));
        }

    }
}
