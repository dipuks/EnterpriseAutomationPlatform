using EAP.Core.Data;
using EAP.Core.DTOs;
using Microsoft.Extensions.Logging;

namespace EAP.Core.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly DeviceRepository _repo;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(DeviceRepository repo, ILogger<DeviceService> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public List<DeviceDto> GetAll()
        {
            _logger.LogInformation("Getting all devices");
            return _repo.GetAll();
        }

        public List<DeviceDto> GetOnline() => _repo.GetOnline();

        public DeviceDto? GetById(int id)
        {
            _logger.LogInformation("Getting device {Id}", id);
            return _repo.GetById(id);
        }

        public DeviceDto Add(CreateDeviceDto dto)
        {
            _logger.LogInformation("Adding device {Name} with status {Status}", dto.Name, dto.Status);
            if (dto.Name.Contains("test", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Device name cannot contain 'test' in production");

            return _repo.Add(dto);
        }

        public DeviceDto? Update(int id, CreateDeviceDto dto)
        {
            _logger.LogWarning("Updating device {Id}", id);
            return _repo.Update(id, dto);
        }

        public bool Delete(int id)
        {
            _logger.LogWarning("Deleting device {Id} - this is critical!", id);
            return _repo.Delete(id);
        }
    }
}