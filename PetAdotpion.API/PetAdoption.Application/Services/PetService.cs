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
            var pets = await _petRepository.GetAllPetsAsync();
            var petDtos = _mapper.Map<IEnumerable<PetDto>>(pets);

            return petDtos;
        }

        public async Task<IEnumerable<DropdownDTO>> GetDropdownAsync()
        {
            return await _petRepository.GetDropdownAsync();
        }

        public async Task<PetDto?> GetPetByIdAsync(int id)
        {
            Pet pet = await _petRepository.GetAsync(x => x.Id == id);
            PetDto dto = _mapper.Map<PetDto>(pet);

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
    }
}