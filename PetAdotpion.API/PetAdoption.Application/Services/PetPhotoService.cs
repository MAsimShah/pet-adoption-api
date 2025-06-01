using AutoMapper;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;
using System.Linq.Expressions;

namespace PetAdoption.Application.Services
{
    public class PetPhotoService(IPetPhotoRepository petPhotoRepository, IMapper mapper) : IPetPhotoService
    {
        private readonly IPetPhotoRepository _petPhotoRepository = petPhotoRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<PetPhotoDTO> GetAsync(Expression<Func<PetPhoto, bool>> predicate = null)
        {
            var entity = await _petPhotoRepository.GetAsync(predicate);
            return _mapper.Map<PetPhotoDTO>(entity);
        }

        public async Task DeletePetPhotoAsync(int id)
        {
            await _petPhotoRepository.DeletePetPhotoAsync(id);
        }


        public async Task SavePetPhotosAsync(int petId, List<string> filePaths)
        {
            var petFileEntities = filePaths.Select(path => new PetPhoto
            {
                PetId = petId,
                PhotoUrl = path,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            await _petPhotoRepository.SavePetPhotosAsync(petFileEntities);
        }
    }
}
