using AutoMapper;
using Rainfall.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rainfall.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<StationReadingItem, StationReadingDto>()
                  .ForMember(dest => dest.measureDate, opt => opt.MapFrom(src => src.dateTime))
                  .ForMember(dest => dest.amount, opt => opt.MapFrom(src => src.value))
                  .ReverseMap();
        }

    }
}
