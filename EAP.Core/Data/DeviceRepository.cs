using System;
using System.Collections.Generic;
using System.Text;

namespace EAP.Core.Data
{
    public class DeviceRepository
    {
        private readonly AppDbContext _context;

        public DeviceRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Device> GetAll => _context.Devices.OrderBy(d => d.Name).ToList();

        public List<Device> GetOnline => _context.Devices.Where(d => d.Status == "Online").ToList();

        public Device Add(Device device)
        {
            _context.Devices.Add(device);
            _context.SaveChanges();
            return device;
        }
    }
}
