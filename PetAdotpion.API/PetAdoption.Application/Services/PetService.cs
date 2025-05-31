using AutoMapper;
using Microsoft.AspNetCore.Http;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;

namespace PetAdoption.Application.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _petRepository;
        private readonly IMapper _mapper;

        public PetService(IPetRepository petRepository, IMapper mapper)
        {
            _petRepository = petRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PetDto>> GetAllPetsAsync()
        {
            var pets = await _petRepository.GetAllPetsAsync(x => x.IsActive == false);
            var petDtos = _mapper.Map<IEnumerable<PetDto>>(pets);

            //if (pets != null && pets.Any(x => x.PetPhotos != null && x.PetPhotos.Any()))
            //{
            //    foreach(var pet in pets)
            //    {
            //        petDtos.First(x => x.Id == pet.Id).PhotoUrls = pet.PetPhotos != null && pet.PetPhotos.Any() ? pet.PetPhotos.Select(x => x.PhotoUrl).ToList() : null;
            //    }
            //}

            return petDtos;
        }

        public async Task<PetDto?> GetPetByIdAsync(int id)
        {
            var pet = await _petRepository.GetAsync(x => x.Id == id);

            var dto = _mapper.Map<PetDto>(pet);

            //if (pet.PetPhotos != null && pet.PetPhotos.Any())
            //{
            //    dto.PhotoUrls = new List<string>();
            //    dto.PhotoUrls = pet.PetPhotos.Select(x => x.PhotoUrl).ToList();
            //}
            return dto;
        }

        public async Task<PetDto> AddPetAsync(PetDto petDto)
        {
            var pet = _mapper.Map<Pet>(petDto);

            await _petRepository.AddPetAsync(pet);

            petDto.Id = pet.Id;

            return petDto; 
        }

        public async Task<PetDto> UpdatePetAsync(PetDto petDto)
        {
            var pet = _mapper.Map<Pet>(petDto);
            pet.UpdatedAt = DateTime.UtcNow;

            await _petRepository.UpdatePetAsync(pet);
            return petDto;
        }

        public async Task DeletePetAsync(int id)
        {
            await _petRepository.DeletePetAsync(id);
        }

        public async Task DeletePetPhotoAsync(int id)
        {
            await _petRepository.DeletePetPhotoAsync(id);
        }


        public async Task SavePetPhotosAsync(int petId, List<string> filePaths)
        {
            var petFileEntities = filePaths.Select(path => new PetPhoto
            {
                PetId = petId,
                PhotoUrl = path,
                CreatedAt = DateTime.UtcNow
            }).ToList();

            await _petRepository.SavePetPhotosAsync(petFileEntities);
        }
    }
}