using AutoMapper;
using PetAdoption.Application.DTO;
using PetAdoption.Domain;

namespace PetAdoption.Application.Mappings
{
    public class PetMap : Profile
    {
        public PetMap()
        {
            CreateMap<Pet, PetDto>().ReverseMap();
        }
    }

    public class PetPhotoMap : Profile
    {
        public PetPhotoMap()
        {
            CreateMap<PetPhoto, PetPhotoDTO>().ReverseMap();
        }
    }
}