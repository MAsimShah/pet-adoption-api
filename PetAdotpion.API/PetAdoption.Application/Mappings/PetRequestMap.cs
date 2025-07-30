using AutoMapper;
using PetAdoption.Application.DTO;
using PetAdoption.Domain;

namespace PetAdoption.Application.Mappings
{
    public class PetRequestMap : Profile
    {
        public PetRequestMap()
        {
            CreateMap<PetRequest, PetRequestDTO>()
             .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.NormalizedUserName : null))
             .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Name : null))
             .ReverseMap();
        }
    }
}
