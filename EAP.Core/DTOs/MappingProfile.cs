using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace EAP.Core.DTOs
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Device, DeviceDto>();
            CreateMap<DeviceDto, Device>();
        }
    }
}
