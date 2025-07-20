using AutoMapper;
using PetAdoption.Application.DTO;
using PetAdoption.Domain;

namespace PetAdoption.Application.Mappings
{
    public class PetRequestMap : Profile
    {
        public PetRequestMap()
        {
            CreateMap<PetRequest, PetRequestDTO>().ReverseMap();
        }
    }
}
