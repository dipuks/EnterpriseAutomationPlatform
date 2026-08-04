using Microsoft.AspNetCore.Mvc;
using EAP.Core.DTOs;
using EAP.Core.Services;

namespace EAP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _service;
        public DevicesController(IDeviceService service) => _service = service;

        [HttpGet]
        [HttpGet]
        public IActionResult Get([FromQuery] string? search)
        {
            var devices = _service.GetAll();

            if (!string.IsNullOrWhiteSpace(search))
                devices = devices.Where(d => d.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(devices);
        }


        [HttpGet("online")]
        public IActionResult GetOnline() => Ok(_service.GetOnline());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var device = _service.GetById(id);
            return device == null ? NotFound($"Device {id} not found") : Ok(device);
        }

        [HttpPost]
        public IActionResult Post(CreateDeviceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = _service.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, CreateDeviceDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updated = _service.Update(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return _service.Delete(id) ? NoContent() : NotFound();
        }
    }
}