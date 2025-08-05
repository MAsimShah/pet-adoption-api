using AutoMapper;
using PetAdoption.Application.DTO;
using PetAdoption.Domain;

namespace PetAdoption.Application.Mappings
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
                .ReverseMap();
        }
    }
}
