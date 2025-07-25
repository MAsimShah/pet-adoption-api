using AutoMapper;
using PetAdoption.Application.DTO;
using PetAdoption.Application.Interfaces;
using PetAdoption.Application.Interfaces.InfrastructureInterfaces;
using PetAdoption.Domain;

namespace PetAdoption.Application.Services
{
    public class PetRequestService(IMapper _mapper, IPetRequestRepository _requestRepository) : IPetRequestService
    {
        public async Task<IEnumerable<PetRequestDTO>> GetAllRequestsAsync()
        {
            var requests = await _requestRepository.GetAllRequestsAsync();
            var petDtos = _mapper.Map<IEnumerable<PetRequestDTO>>(requests);

            return petDtos;
        }

        public async Task<PetRequestDTO?> GetRequestByIdAsync(int id)
        {
            PetRequest pet = await _requestRepository.GetAsync(x => x.Id == id);
            PetRequestDTO dto = _mapper.Map<PetRequestDTO>(pet);

            return dto;
        }

        public async Task<PetRequestDTO> AddRequestAsync(PetRequestDTO requestDto)
        {
            var request = _mapper.Map<PetRequest>(requestDto);

            await _requestRepository.AddRequestAsync(request);

            requestDto.Id = request.Id;

            return requestDto;
        }

        public async Task<PetRequestDTO> UpdateRequestAsync(PetRequestDTO requestDto)
        {
            var request = _mapper.Map<PetRequest>(requestDto);
            request.UpdatedAt = DateTime.UtcNow;

            await _requestRepository.UpdateRequestAsync(request);
            return requestDto;
        }

        public async Task DeleteRequestAsync(int id)
        {
            await _requestRepository.DeleteRequestAsync(id);
        }
    }
}
