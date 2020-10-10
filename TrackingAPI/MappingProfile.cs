using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrackingAPI.Controllers;
using TrackingAPI.DTO;
using TrackingLib.Models;

namespace TrackingAPI
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {            
            CreateMap<UploadInfoRequest, DocumentInfo>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DocumentName))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.DocumentCategory));
            CreateMap<ProcessInfoRequest, ProcessInfo>();
        }
    }
}
