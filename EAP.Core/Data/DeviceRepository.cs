using EAP.Core.DTOs;

namespace EAP.Core.Data
{
    public class DeviceRepository
    {
        private readonly AppDbContext _db;
        public DeviceRepository(AppDbContext db) => _db = db;

        private static DeviceDto ToDto(Device d) => new DeviceDto
        {
            Id = d.Id,
            Name = d.Name,
            Status = d.Status,
            LastSeen = d.LastSeen
        };

        public List<DeviceDto> GetAll() =>
            _db.Devices.OrderBy(d => d.Name).AsEnumerable().Select(d => ToDto(d)).ToList();

        public List<DeviceDto> GetOnline() =>
            _db.Devices.Where(d => d.Status == "Online").AsEnumerable().Select(d => ToDto(d)).ToList();

        public DeviceDto? GetById(int id)
        {
            var d = _db.Devices.Find(id);
            return d == null ? null : ToDto(d);
        }

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

        public DeviceDto? Update(int id, CreateDeviceDto dto)
        {
            var entity = _db.Devices.Find(id);
            if (entity == null) return null;
            entity.Name = dto.Name;
            entity.Status = dto.Status;
            entity.LastSeen = DateTime.UtcNow;
            _db.SaveChanges();
            return ToDto(entity);
        }

        public bool Delete(int id)
        {
            var entity = _db.Devices.Find(id);
            if (entity == null) return false;
            _db.Devices.Remove(entity);
            _db.SaveChanges();
            return true;
        }
    }
}