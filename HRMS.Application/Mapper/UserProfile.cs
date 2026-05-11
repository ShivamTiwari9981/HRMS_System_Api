using AutoMapper;
using HRMS.Application.DTOs;

namespace HRMS.Application.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            // Maps User (Entity) to UserDto
            CreateMap<UserDto, LoginResponseDto>().ReverseMap();

            // Optional: Custom mapping for specific properties
            // .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
        }
    }
}
