using AutoMapper;
using PetAdoption.Application.DTO;
using PetAdoption.Domain;

namespace PetAdoption.Application.Mappings
{
    public class PetRequestMap : Profile
    {
        public PetRequestMap()
        {
            CreateMap<PetRequestDTO, PetRequest>()
                .ForMember(dest => dest.Pet, opt => opt.Ignore())   // prevent Pet from being created
                .ForMember(dest => dest.User, opt => opt.Ignore()); // prevent User from being created

            CreateMap<PetRequest, PetRequestDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.NormalizedUserName : null))
                .ForMember(dest => dest.PetName, opt => opt.MapFrom(src => src.Pet != null ? src.Pet.Name : null));
        }
    }
}
