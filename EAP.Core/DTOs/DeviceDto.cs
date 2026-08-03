using System;
using System.Collections.Generic;
using System.Text;

namespace EAP.Core.DTOs
{
    public class DeviceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public  string Status { get; set; } = string.Empty;
        public DateTime LastSeen { get; set; }
    }
}
