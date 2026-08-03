using EAP.Core.DTOs;

namespace EAP.Core.Services
{
    public interface IDeviceService
    {
        List<DeviceDto> GetAll();
        List<DeviceDto> GetOnline();
        DeviceDto? GetById(int id);
        DeviceDto Add(CreateDeviceDto dto);
        DeviceDto? Update(int id, CreateDeviceDto dto);
        bool Delete(int id);
    }
}