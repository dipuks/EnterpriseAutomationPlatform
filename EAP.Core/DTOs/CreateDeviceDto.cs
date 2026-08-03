using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EAP.Core.DTOs
{
    public class CreateDeviceDto
    {
        [Required, StringLength(100, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        
        [Required]
        [RegularExpression("Online|Offline|Maintenance", ErrorMessage = "Status must be either 'Online', 'Offline', or 'Maintenance'.")]
        public string Status { get; set; } = "Online";
    }
}
