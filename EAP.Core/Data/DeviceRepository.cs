using EAP.Core.DTOs;

namespace EAP.Core.Data
{
    public class DeviceRepository
    {
        private readonly AppDbContext _db;
        public DeviceRepository(AppDbContext db) => _db = db;

        private DeviceDto ToDto(Device d) => new DeviceDto
        {
            Id = d.Id,
            Name = d.Name,
            Status = d.Status,
            LastSeen = d.LastSeen
        };

        public List<DeviceDto> GetAll() =>
            _db.Devices.OrderBy(d => d.Name).Select(d => ToDto(d)).ToList();

        public List<DeviceDto> GetOnline() =>
            _db.Devices.Where(d => d.Status == "Online").Select(d => ToDto(d)).ToList();

        public DeviceDto Add(CreateDeviceDto dto)
        {
            var entity = new Device
            {
                Name = dto.Name,
                Status = dto.Status,
                LastSeen = DateTime.UtcNow
            };
            _db.Devices.Add(entity);
            _db.SaveChanges();
            return ToDto(entity);
        }
    }
}