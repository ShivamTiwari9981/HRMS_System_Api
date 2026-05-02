using AutoMapper;
using HRMS.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Simple mapping: Source -> Destination
            //CreateMap<Client, ClientDto>().ReverseMap();

            // Mapping with custom logic or different property names
            //CreateMap<Order, OrderDto>()
            //    .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            //    .ReverseMap(); // Allows two-way mapping
        }
    }
}
